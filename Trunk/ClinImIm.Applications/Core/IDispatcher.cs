using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinImIm.Applications.Core
{
    public interface IDispatcher
    {
        void Dispatch(Action actionToInvoke);

        TResult Dispatch<TResult>(Func<TResult> actionToInvoke);

        bool DispatchRequired();

        /// <summary>
        /// This was introduced purely for testing.  Anywhere you are about to kick off a background thread to use the dispatcher,
        /// check this to see if you should be doing it in a background thread or not first.  The test/mock implementation should be false so your tests
        /// are not trying to test multithreaded code.
        /// </summary>
        bool AllowMultiThreading { get; }
    }
}
