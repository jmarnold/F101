using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace F101.Scratchpad
{
    public class InvokeActionCallBehavior<TController, TInput, TOutput> : IActionBehavior
        where TController : class
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TController, TInput, TOutput> _action;
        private readonly TController _controller;
        private readonly IFubuRequest _request;
        private readonly IActionBehavior _inner;

        public InvokeActionCallBehavior(Func<TController, TInput, TOutput> action, TController controller, 
            IFubuRequest request, IActionBehavior inner)
        {
            _action = action;
            _controller = controller;
            _request = request;
            _inner = inner;
        }

        public void Invoke()
        {
            var input = _request.Get<TInput>();
            var output = _action(_controller, input);
            _request.Set(output);

            if(_inner != null)
            {
                // open a transaction
                _inner.Invoke();
                // commit the transaction
            }
        }

        public void InvokePartial()
        {
            Invoke();
        }
    }

    public class OutputBehavior<TOutput> : IActionBehavior
        where TOutput : class
    {
        private readonly IFubuRequest _request;

        public OutputBehavior(IFubuRequest request)
        {
            _request = request;
        }

        public void Invoke()
        {
            var output = _request.Get<TOutput>();
            // render...
        }

        public void InvokePartial()
        {
            Invoke();
        }
    }
}