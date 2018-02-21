using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shengtai
{
    internal class ExclusiveSynchronizationContext : SynchronizationContext
    {
        public override void Send(SendOrPostCallback d, object state)
        {
            throw new NotSupportedException("We cannot send to our same thread");
        }

        readonly Queue<Tuple<SendOrPostCallback, object>> items = new Queue<Tuple<SendOrPostCallback, object>>();
        readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
        public override void Post(SendOrPostCallback d, object state)
        {
            lock (items)
                items.Enqueue(Tuple.Create(d, state));

            workItemsWaiting.Set();
        }

        private bool done;
        public void EndMessageLoop()
        {
            Post(_ => done = true, null);
        }

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
    }
}
