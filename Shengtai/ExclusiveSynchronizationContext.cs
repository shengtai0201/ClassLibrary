using System;
using System.Collections.Generic;
using System.Threading;

namespace Shengtai
{
    internal class ExclusiveSynchronizationContext : SynchronizationContext, IDisposable
    {
        private readonly Queue<Tuple<SendOrPostCallback, object>> items = new Queue<Tuple<SendOrPostCallback, object>>();

        private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);

        private bool done;

        public void BeginMessageLoop()
        {
            while (!done)
            {
                Tuple<SendOrPostCallback, object> task = null;
                lock (items)
                {
                    if (items.Count > 0)
                    {
                        task = items.Dequeue();
                    }
                }
                if (task != null)
                {
                    task.Item1(task.Item2);
                }
                else
                {
                    workItemsWaiting.WaitOne();
                }
            }
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public void EndMessageLoop()
        {
            Post(_ => done = true, null);
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            lock (items)
                items.Enqueue(Tuple.Create(d, state));

            workItemsWaiting.Set();
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            throw new NotSupportedException("We cannot send to our same thread");
        }
    }
}