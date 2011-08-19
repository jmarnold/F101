using System.Transactions;
using FubuMVC.Core.Behaviors;

namespace F101.Scratchpad
{
    public class MyTransactionBehavior : IActionBehavior
    {
        private readonly IActionBehavior _inner;

        public MyTransactionBehavior(IActionBehavior inner)
        {
            _inner = inner;
        }

        public void Invoke()
        {
            using(var scope = new TransactionScope())
            {
                if(_inner != null)
                {
                    _inner.Invoke();
                }

                scope.Complete();
            }
        }

        public void InvokePartial()
        {
            if(_inner != null)
            {
                _inner.InvokePartial();
            }
        }
    }
}