using IoCContainerComparisonTests.Adapters.Abstraction;
using IoCContainerComparisonTests.Adapters.Concrete;
using IoCContainerComparisonTests.Adapters.Enumeration;
using IoCContainerComparisonTests.Mock;
using IoCContainerComparisonTests.Mock.Abstraction;
using IoCContainerComparisonTests.Mock.Concrete;
using IoCContainerComparisonTests.Mock.Concrete.Exodus;
using IoCContainerComparisonTests.Mock.Concrete.Unity;
using Ninject.Activation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerComparisonTests
{
    public class UnityContainerTests : ContainerTests<UnityContainerWrapper>
    {
        protected override void SetupCustomScope(UnityContainerWrapper container)
        {
            container.CreateCustomScopeFunc = context => new CustomScopeLifetimeManager();
        }
    }

    public class ExodusContainerTests : ContainerTests<ExodusContainerWrapper>
    {
        protected override void SetupCustomScope(ExodusContainerWrapper container)
        {
            container.CreateCustomScopeFunc = context => new CustomScopeGovernor();
        }
    }

    public class NinjectTests : ContainerTests<NinjectWrapper>
    {
        protected override void SetupCustomScope(NinjectWrapper container)
        {
            container.CreateCustomScopeFunc = context => CustomScope.Current;
        }
    }

    public abstract class ContainerTests<T> where T : IContainerAdapterWrapper, new()
    {
        private T _container;

        [SetUp]
        public void Setup()
        {
            _container = new T();
            SetupCustomScope(_container);
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        protected abstract void SetupCustomScope(T container);

        [Test]
        public void ContainerResolvesTypeAsSingleton()
        {
            _container.Register(typeof(IMockInterface), typeof(MockClass), AdapterScope.Singleton);
            object testObject11 = _container.Resolve(typeof(IMockInterface));
            object testObject12 = _container.Resolve(typeof(IMockInterface));
            Assert.AreSame(testObject11, testObject12);
        }

        [Test]
        public void ContainerResolvesTypeAsTransient()
        {
            _container.Register(typeof(IMockInterface), typeof(MockClass), AdapterScope.Transient);
            object testObject11 = _container.Resolve(typeof(IMockInterface));
            object testObject12 = _container.Resolve(typeof(IMockInterface));
            Assert.AreNotSame(testObject11, testObject12);
        }
    }
}
