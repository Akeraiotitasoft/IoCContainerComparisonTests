using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Mock
{
    public class CustomScope : ICustomScopeBag, IDisposable
    {
        [ThreadStatic]
        private static CustomScope _current;
        public static CustomScope Current { get => _current; set => _current = value; }
        public Dictionary<object, object> Bag { get; } = new Dictionary<object, object>();

        // The reason for this is the following article
        // https://stackoverflow.com/questions/937181/c-sharp-pattern-to-prevent-an-event-handler-hooked-twice
        private Action<object> notifyDisposed;

        /// <summary>
        /// NotifyDisposed - Safe to keep adding the event
        /// </summary>
        public event Action<object> NotifyDisposed
        {
            add
            {
                if (notifyDisposed == null || !notifyDisposed.GetInvocationList().Contains(value))
                {
                    notifyDisposed += value;
                }
            }
            remove
            {
                notifyDisposed -= value;
            }
        }

        public void Dispose()
        {
            notifyDisposed?.Invoke(this);
        }
    }

    public interface ICustomScopeBag
    {
        Dictionary<object, object> Bag { get; }
    }
}
