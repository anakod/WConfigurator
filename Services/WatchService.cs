using Blazm.Bluetooth;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WConfigurator.Data;

namespace WConfigurator.Services
{
    public class WatchService
    {
        Device device;
        Guid serviceId = Guid.Parse("6e400001-b5a3-f393-e0a9-e50e24dcca9e");
        Guid writeCharacteristicId = Guid.Parse("6e400002-b5a3-f393-e0a9-e50e24dcca9e");
        Guid readCharacteristicId = Guid.Parse("6e400003-b5a3-f393-e0a9-e50e24dcca9e");
        static readonly char[] SkipSpecialChars = "\x03 \x10\t\r\n".ToArray();

        List<TransmissionHandler> handlers = new List<TransmissionHandler>();
        StringBuilder current = new StringBuilder();

        public BluetoothNavigator Navigator { get; set; }
        public bool IsConnected { get; private set; }
        public bool IsWaitingForConnection { get; private set; }

        internal event Action OnBeforeCommand;
        public event Action<byte[]> OnDataReceived;
        public MulticastSingleInstanceEventCallback OnUpdate;

        public void AddHandler<T>(string app, Func<T, bool> handler)
        {
            AddHandler<T>(app, null, handler);
        }
        public void AddHandler<T>(string app, string[] commands, Func<T, bool> handler)
        {
            var listener = new Listener<T>(handler);
            handlers.Add(new TransmissionHandler(app, commands, listener));
        }


        public WatchService(BluetoothNavigator navigator)
        {
            Navigator = navigator;
        }

        public virtual async Task Connect()
        {
            IsWaitingForConnection = true;
            Updated();

            var query = new RequestDeviceQuery();
            query.Filters.Add(new Filter() { Services = { serviceId } });
            device = await Navigator.RequestDeviceAsync(query);

            await Navigator.SetupNotifyAsync(device.Id, serviceId, readCharacteristicId);
            Navigator.Notification += Value_Notification;
            
            IsConnected = true;
            Updated();
        }

        public async Task ExecuteCommand(AppRequest command)
        {
            string json = JsonConvert.SerializeObject(command);
            await ExecuteCommand(json);
        }

        private void Value_Notification(object sender, CharacteristicEventArgs e)
        {
            lock (this)
            {
                if (OnDataReceived != null)
                    OnDataReceived.Invoke(e.Value);
                var msg = Encoding.UTF8.GetString(e.Value);
                int p = msg.IndexOf('\x03');
                if (p != -1)
                {
                    var sub = msg.Substring(0, p);
                    current.Append(sub);
                    var data = current.ToString().Trim(SkipSpecialChars);
                    current.Clear();
                    if (handlers.Count > 0 && data.Length > 0)
                    {
                        bool update = Process(data);
                        if (update)
                            Updated();
                    }

                    current.Append(msg.Substring(p).TrimStart(SkipSpecialChars));
                }
                else
                {
                    current.Append(msg);
                }
            }
        }

        private bool Process(string msg)
        {
            var result = false;
            JObject data;
            try
            {
                data = JObject.Parse(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(msg);
                return false;
            }
            if (data != null && data.ContainsKey("app"))
            {
                for (int i = 0; i < handlers.Count; i++)
                {
                    var handler = handlers[i];
                    var key = data["app"].ToString();
                    if (handler.Application == key)
                    {
                        var response = data.ContainsKey("r") ? data["r"].ToString() : "";
                        if (!handler.HasCommandFilter || handler.Commands.Contains(response))
                        {
                            result |= handler.Listener.Execute(msg);
                            if (handler.RemoveAfterFire)
                            {
                                handlers.Remove(handler);
                                i--;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void Updated()
        {
            if (OnUpdate != null)
                OnUpdate.InvokeAsync();
        }

        public async Task ExecuteCommand(string command)
        {
            var data = Encoding.UTF8.GetBytes(command);
            await ExecuteCommand(data);
        }
        public async Task ExecuteCommand(byte[] command)
        {
            if (OnBeforeCommand != null)
                OnBeforeCommand();
            var data = new byte[command.Length + 2];
            Array.Copy(command, data, command.Length);
            data[command.Length] = 10; // == \n
            data[command.Length + 1] = 3; // == \x03
            await Navigator.WriteValueAsync(device.Id, serviceId, writeCharacteristicId, data);
        }
    }
}
