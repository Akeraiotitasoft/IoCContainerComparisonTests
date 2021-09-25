using IoCContainerComparisonTests.Adapters.Abstraction;
using IoCContainerComparisonTests.Adapters.Enumeration;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Adapters.Concrete
{
    public class NinjectWrapper : IContainerAdapterWrapper
    {
        private readonly IKernel _kernel;
        public NinjectWrapper()
        {
            _kernel = new StandardKernel();
        }

        public Func<IAdapterContext, object> CreateCustomScopeFunc { get; set;}

        public IContainerAdapterScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }

        protected virtual void CallPerRequestScope(Func<IBindingWhenInNamedWithOrOnSyntax<object>> register)
        {
            throw new InvalidOperationException("Not supported");
        }

        public void Register(Type from, Type to, AdapterScope scope)
        {
            Func<IBindingWhenInNamedWithOrOnSyntax<object>> register = () => _kernel.Bind(from).To(to);
            switch (scope)
            {
                case AdapterScope.Singleton:
                    register().InSingletonScope();
                    break;
                case AdapterScope.Transient:
                    register().InTransientScope();
                    break;
                case AdapterScope.Custom:
                    Func<IContext, object> func = context =>
                    {
                        AdapterContext adapterContext = new AdapterContext()
                        {
                            Container = this,
                            WrappedContainer = _kernel,
                            WrappedContext = context,
                            FromType = from,
                            ToType = to,
                            Name = null
                        };
                        return CreateCustomScopeFunc(adapterContext);
                    };
                    register().InScope(func);
                    break;
                case AdapterScope.PerThread:
                    register().InThreadScope();
                    break;
                case AdapterScope.PerRequest:
                    CallPerRequestScope(register);
                    break;
                default:
                    throw new InvalidOperationException("Not supported");
            }
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
            return _kernel.Get(type);
        }

        public object Resolve(Type type, string name)
        {
            throw new NotImplementedException();
        }
    }
}
