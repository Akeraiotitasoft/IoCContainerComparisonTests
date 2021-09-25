using IoCContainerComparisonTests.Adapters.Abstraction;
using IoCContainerComparisonTests.Adapters.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace IoCContainerComparisonTests.Adapters.Concrete
{
    public class UnityContainerWrapper : IContainerAdapterWrapper
    {
        private readonly IUnityContainer _unityContainer;

        public UnityContainerWrapper()
        {
            _unityContainer = new UnityContainer();
        }

        private UnityContainerWrapper(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public Func<IAdapterContext, object> CreateCustomScopeFunc { get; set; }

        public IContainerAdapterScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }

        protected virtual ITypeLifetimeManager GetPerRequestLifetimeManager()
        {
            throw new InvalidOperationException("Not valid in this test harness");
        }

        public void Register(Type from, Type to, AdapterScope scope)
        {
            ITypeLifetimeManager typeLifetimeManager;
            switch (scope)
            {
                case AdapterScope.Singleton:
                    typeLifetimeManager = new ContainerControlledLifetimeManager();
                    break;
                case AdapterScope.Transient:
                    typeLifetimeManager = new TransientLifetimeManager();
                    break;
                case AdapterScope.Custom:
                    AdapterContext adapterContext = new AdapterContext()
                    {
                        Container = this,
                        WrappedContainer = _unityContainer,
                        WrappedContext = null,
                        FromType = from,
                        ToType = to,
                        Name = null
                    };
                    typeLifetimeManager = (ITypeLifetimeManager)this.CreateCustomScopeFunc(adapterContext);
                    break;
                case AdapterScope.PerRequest:
                    typeLifetimeManager = GetPerRequestLifetimeManager();
                    break;
                default:
                    throw new InvalidOperationException("The scope is an invalid operation");
            }
            _unityContainer.RegisterType(from, to, typeLifetimeManager);
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
            return _unityContainer.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            throw new NotImplementedException();
        }
    }
}
