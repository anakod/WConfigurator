using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WConfigurator.Data
{
    public interface IListener
    {
        bool Execute(string message);
    }

    public class Listener<T> : IListener
    {
        Func<T, bool> OnAction;

        public Listener(Func<T, bool> action)
        {
            OnAction = action;
        }

        public bool Execute(string message)
        {
            var value = JsonConvert.DeserializeObject<T>(message);
            if (OnAction != null)
                return OnAction(value);
            return false;
        }
    }

    public class TransmissionHandler
    {
        public TransmissionHandler(string app, string command, IListener listener)
            : this(app, !string.IsNullOrEmpty(command) ? new[] { command } : null, listener)
        {
        }

        public TransmissionHandler(string app, string[] commands, IListener listener)
        {
            Application = app;
            Commands = commands;
            Listener = listener;
        }

        public string Application { get; set; }
        public string[] Commands { get; set; }
        public IListener Listener { get; set; }

        public bool RemoveAfterFire { get; set; } = false;

        public bool HasCommandFilter => Commands != null;
    }

    public class AppBaseOperation
    {
        /// <summary>
        /// Operation type.
        /// For request: 'req', for configuration: 'conf'
        /// </summary>
        [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = "";

        /// <summary>
        /// Related application
        /// </summary>
        [JsonProperty("app")]
        public string Application { get; set; }
    }

    public class AppRequest : AppBaseOperation
    {
        /// <summary>
        /// Command type
        /// </summary>
        [JsonProperty("r")]
        public string Command { get; set; }
    }
}
