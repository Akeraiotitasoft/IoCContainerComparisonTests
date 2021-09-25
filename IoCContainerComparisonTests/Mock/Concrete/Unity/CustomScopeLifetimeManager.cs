using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;

// code based on this
// https://github.com/unitycontainer/aspnet-mvc/blob/master/src/PerRequestLifetimeManager.cs


namespace IoCContainerComparisonTests.Mock.Concrete.Unity
{
    public class CustomScopeLifetimeManager : LifetimeManager, ITypeLifetimeManager
    {
        private readonly object _lifetimeKey = new object();

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return new CustomScopeLifetimeManager();
        }

        public override object GetValue(ILifetimeContainer container = null)
        {
            return CustomModule.GetValue(_lifetimeKey);
        }

        public override void RemoveValue(ILifetimeContainer container = null)
        {
            var disposable = GetValue() as IDisposable;

            disposable?.Dispose();

            CustomModule.SetValue(_lifetimeKey, null);
        }

        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            CustomModule.SetValue(_lifetimeKey, newValue);
        }
    }
}
