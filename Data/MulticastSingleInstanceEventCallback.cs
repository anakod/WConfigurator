using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WConfigurator.Data
{
    /// <summary>
    /// Multicast EventCallback with single instance per target *type*. Previous references will be automatically removed when new added
    /// </summary>
    public class MulticastSingleInstanceEventCallback : MulticastEventCallback
    {
        public MulticastSingleInstanceEventCallback()
        {
        }
        internal MulticastSingleInstanceEventCallback(List<EventCallbackHandler> list) : base(list)
        {
        }

        public static MulticastSingleInstanceEventCallback operator +(MulticastSingleInstanceEventCallback left, Action callback)
        {
            var added = new EventCallbackHandler(callback);
            return AddHandler(left, added);
        }

        public static MulticastSingleInstanceEventCallback operator +(MulticastSingleInstanceEventCallback left, Func<Task> callback)
        {
            var added = new EventCallbackHandler(callback);
            return AddHandler(left, added);
        }

        public static MulticastSingleInstanceEventCallback operator +(MulticastSingleInstanceEventCallback left, IHandleEvent receiver)
        {
            var added = new EventCallbackHandler(receiver);
            return AddHandler(left, added);
        }

        public static MulticastSingleInstanceEventCallback operator -(MulticastSingleInstanceEventCallback left, IHandleEvent receiver)
        {
            var list = new List<EventCallbackHandler>();
            if (left != null)
            {
                var filtered = left.eventCallbacks.Where(x => x.Receiver != receiver);
                list.AddRange(filtered);
            }

            return new MulticastSingleInstanceEventCallback(list);
        }

        private static MulticastSingleInstanceEventCallback AddHandler(MulticastSingleInstanceEventCallback left, EventCallbackHandler added)
        {
            var list = new List<EventCallbackHandler>();
            if (left != null)
            {
                var actual = left.eventCallbacks.Where(x => x.Receiver.GetType() != added.Receiver.GetType());
                list.AddRange(actual);
            }

            if (!list.Select(x => x.Receiver).Contains(added.Receiver))
                list.Add(added);

            return new MulticastSingleInstanceEventCallback(list);
        }
    }
}
