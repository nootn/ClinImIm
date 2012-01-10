using System;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using ClinImIm.Applications.Core;

namespace ClinImIm.Presentation.Infrastructure
{
    /// <summary>
    /// Thanks Paul Stovell: http://bindablelinq.codeplex.com/SourceControl/changeset/view/25046#293214
    /// </summary>
    [Export(typeof(IDispatcher))]
    public class WpfDispatcher : IDispatcher
    {
        private readonly Dispatcher _dispatcher;

        public WpfDispatcher()
            : this(Dispatcher.CurrentDispatcher)
        {
        }

        public WpfDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Dispatch(Action actionToInvoke)
        {
            if (!DispatchRequired())
            {
                actionToInvoke();
            }
            else
            {
                _dispatcher.Invoke(DispatcherPriority.Normal, actionToInvoke);
            }
        }

        public TResult Dispatch<TResult>(Func<TResult> actionToInvoke)
        {
            if (_dispatcher.CheckAccess())
            {
                return actionToInvoke();
            }
            return (TResult)_dispatcher.Invoke(DispatcherPriority.Normal, actionToInvoke);
        }

        public bool DispatchRequired()
        {
            return !_dispatcher.CheckAccess();
        }

        public bool AllowMultiThreading
        {
            get { return true; }
        }
    }
}
