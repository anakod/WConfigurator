using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WConfigurator.Data
{
    public class MulticastEventCallback
    {
        internal List<EventCallbackHandler> eventCallbacks = new List<EventCallbackHandler>();

        public MulticastEventCallback()
        {
        }

        internal MulticastEventCallback(List<EventCallbackHandler> list)
        {
            eventCallbacks = list;
        }

        public static MulticastEventCallback operator +(MulticastEventCallback left, Action callback)
        {
            var added = new EventCallbackHandler(callback);
            return AddHandler(left, added);
        }

        public static MulticastEventCallback operator +(MulticastEventCallback left, Func<Task> callback)
        {
            var added = new EventCallbackHandler(callback);
            return AddHandler(left, added);
        }

        public static MulticastEventCallback operator +(MulticastEventCallback left, IHandleEvent receiver)
        {
            var added = new EventCallbackHandler(receiver);
            return AddHandler(left, added);
        }

        public static MulticastEventCallback operator -(MulticastEventCallback left, IHandleEvent receiver)
        {
            var list = new List<EventCallbackHandler>();
            if (left != null)
            {
                var filtered = left.eventCallbacks.Where(x => x.Receiver != receiver);
                list.AddRange(filtered);
            }

            return new MulticastEventCallback(list);
        }

        private static MulticastEventCallback AddHandler(MulticastEventCallback left, EventCallbackHandler added)
        {
            var list = new List<EventCallbackHandler>();
            if (left != null)
                list.AddRange(left.eventCallbacks);

            if (!list.Select(x => x.Receiver).Contains(added.Receiver))
                list.Add(added);

            return new MulticastEventCallback(list);
        }

        public async Task InvokeAsync()
        {
            foreach (var it in eventCallbacks)
            {
                await it.InvokeAsync();
            }
        }
    }

    internal struct EventCallbackHandler
    {
        public EventCallbackHandler(MulticastDelegate callback)
        {
            Callback = callback;
            Receiver = callback.Target as IHandleEvent;
        }

        public EventCallbackHandler(IHandleEvent handler)
        {
            Callback = null;
            Receiver = handler;
        }

        public static IHandleEvent GetReceiver(EventCallback callback)
        {
            var prop = callback.GetType().GetField("Receiver", BindingFlags.NonPublic | BindingFlags.Instance);
            return prop.GetValue(callback) as IHandleEvent;
        }

        public Task InvokeAsync()
        {
            if (HasDelegate)
                return Receiver.HandleEventAsync(new EventCallbackWorkItem(Callback), null);
            else if (Receiver != null)
                return Receiver.HandleEventAsync(new EventCallbackWorkItem(), null);

            return Task.CompletedTask;
        }

        public MulticastDelegate Callback { get; }
        public IHandleEvent Receiver { get; }
        public bool HasDelegate => Callback != null;
    }
}
