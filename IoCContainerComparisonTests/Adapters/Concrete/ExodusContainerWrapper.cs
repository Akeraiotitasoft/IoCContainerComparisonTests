using Akeraiotitasoft.ExodusContainer;
using Akeraiotitasoft.ExodusContainer.Governor;
using IoCContainerComparisonTests.Adapters.Abstraction;
using IoCContainerComparisonTests.Adapters.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Adapters.Concrete
{
    public class ExodusContainerWrapper : IContainerAdapterWrapper
    {
        private readonly IExodusContainer _exodusContainer;

        public ExodusContainerWrapper()
        {
            _exodusContainer = new ExodusContainer();
        }

        public Func<IAdapterContext, object> CreateCustomScopeFunc { get; set; }

        public IContainerAdapterScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _exodusContainer.Dispose();
        }

        protected virtual IScopeGovernor CreatePerRequestScopeGovernor()
        {
            throw new NotSupportedException("Not Supported.  Use a different test harness");
        }

        public void Register(Type from, Type to, AdapterScope scope)
        {
            IScopeGovernor scopeGovernor;
            switch (scope)
            {
                case AdapterScope.Singleton:
                    scopeGovernor = new SingletonGovernor();
                    break;
                case AdapterScope.Transient:
                    scopeGovernor = new TransientGovernor();
                    break;
                case AdapterScope.PerThread:
                    scopeGovernor = new PerThreadGovernor();
                    break;
                case AdapterScope.Scoped:
                    throw new NotSupportedException("TODO: add the scoped type");
                case AdapterScope.PerRequest:
                    scopeGovernor = CreatePerRequestScopeGovernor();
                    break;
                case AdapterScope.Custom:
                    AdapterContext adapterContext = new AdapterContext()
                    {
                        Container = this,
                        WrappedContainer = _exodusContainer,
                        WrappedContext = null,
                        FromType = from,
                        ToType = to,
                        Name = null
                    };
                    scopeGovernor = (IScopeGovernor)this.CreateCustomScopeFunc(adapterContext);
                    break;
                default:
                    throw new NotSupportedException("Not Supported");
            }
            _exodusContainer.Register(from, to, scopeGovernor);
        }

        public void Register(Type from, Type to, string name, AdapterScope scope)
        {
            throw new NotImplementedException();
        }

        public void Register(Type from, AdapterScope scope, Func<IAdapterContext, object> createFunc)
        {
            throw new NotImplementedException();
        }

        public void Register(Type from, string name, AdapterScope scope, Func<IAdapterContext, object> createFunc)
        {
            throw new NotImplementedException();
        }

        public void RegisterConstant(Type from, object value)
        {
            throw new NotImplementedException();
        }

        public void RegisterConstant(Type from, string name, object value)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            return _exodusContainer.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            throw new NotImplementedException();
        }
    }
}
