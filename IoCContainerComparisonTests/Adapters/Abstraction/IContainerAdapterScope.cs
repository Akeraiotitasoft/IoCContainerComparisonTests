using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerComparisonTests.Adapters.Abstraction
{
    public interface IContainerAdapterScope : IDisposable
    {
        object Resolve(Type type);

        object Resolve(Type type, string name);
    }
}
