using IoCContainerComparisonTests.Adapters.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerComparisonTests.Adapters.Abstraction
{
    public interface IContainerAdapterWrapper : IContainerAdapterScope, IDisposable
    {
        void Register(Type from, Type to, AdapterScope scope);
        void Register(Type from, Type to, string name, AdapterScope scope);

        void Register(Type from, AdapterScope scope, Func<IAdapterContext, object> createFunc);

        void Register(Type from, string name, AdapterScope scope, Func<IAdapterContext, object> createFunc);

        void RegisterConstant(Type from, object value);

        void RegisterConstant(Type from, string name, object value);

        IContainerAdapterScope BeginScope();

        Func<IAdapterContext, object> CreateCustomScopeFunc { get; set; }
    }
}
