using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClinImIm.Applications.Core;

namespace ClinImIm.Applications.Test.Mocks
{

    /// <summary>
    /// Thanks Paul Stovell: http://bindablelinq.codeplex.com/SourceControl/changeset/view/25046#299606
    /// </summary>
    internal class MockDispatcher : IDispatcher
    {
        private readonly List<Thread> _dispatcherThreads = new List<Thread>();
        private readonly Thread _creationThread = Thread.CurrentThread;

        public MockDispatcher()
        {
        }

        public void Dispatch(Action actionToInvoke)
        {
            if (DispatchRequired())
            {
                var reset = new AutoResetEvent(false);
                var thread = new Thread(
                    ignored =>
                    {
                        actionToInvoke();
                        reset.Set();
                    });
                _dispatcherThreads.Add(thread);
                thread.Start();
                reset.WaitOne();
            }
            else
            {
                actionToInvoke();
            }
        }

        public TResult Dispatch<TResult>(Func<TResult> actionToInvoke)
        {
            if (DispatchRequired())
            {
                var result = default(TResult);
                var reset = new AutoResetEvent(false);
                var thread = new Thread(
                    ignored =>
                    {
                        result = actionToInvoke();
                        reset.Set();
                    });
                _dispatcherThreads.Add(thread);
                thread.Start();
                reset.WaitOne();
                return result;
            }
            return actionToInvoke();
        }

        public bool DispatchRequired()
        {
            return
                Thread.CurrentThread != _creationThread
                && _dispatcherThreads.Contains(Thread.CurrentThread) == false;
        }


        public bool AllowMultiThreading
        {
            get { return false; }
        }
    }
}
