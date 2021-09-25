using IoCContainerComparison.Web.Tests.Adapters.Concerete;
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
    public class UnityContainerTests : ContainerWebTests<UnityContainerWebWrapper>
    {
        protected override void SetupCustomScope(UnityContainerWebWrapper container)
        {
            container.CreateCustomScopeFunc = context => new CustomScopeLifetimeManager();
        }
    }

    public class ExodusContainerTests : ContainerWebTests<ExodusContainerWebWrapper>
    {
        protected override void SetupCustomScope(ExodusContainerWebWrapper container)
        {
            container.CreateCustomScopeFunc = context => new CustomScopeGovernor();
        }
    }

    public class NinjectTests : ContainerWebTests<NinjectWebWrapper>
    {
        protected override void SetupCustomScope(NinjectWebWrapper container)
        {
            container.CreateCustomScopeFunc = context => CustomScope.Current;
        }
    }

    public abstract class ContainerWebTests<T> where T : IContainerAdapterWrapper, new()
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
        public void ContainerResolvesTypeAsPerRequest()
        {
            _container.Register(typeof(IMockInterface), typeof(MockClass), AdapterScope.PerRequest);

            MockWebServer.GetMockHttpContext();

            object testObject11 = _container.Resolve(typeof(IMockInterface));

            MockWebServer.GetMockHttpContext();

            object testObject12 = _container.Resolve(typeof(IMockInterface));
            Assert.AreNotSame(testObject11, testObject12);
        }
    }
}
