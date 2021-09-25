using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Adapters.Abstraction
{
    public interface IAdapterContext
    {
        IContainerAdapterWrapper Container { get; }

        object WrappedContainer { get; }

        object WrappedContext { get; }

        Type FromType { get; }

        Type ToType { get; }

        string Name { get; }
    }
}
