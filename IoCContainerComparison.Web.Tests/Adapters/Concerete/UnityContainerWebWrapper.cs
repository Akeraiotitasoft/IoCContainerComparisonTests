using IoCContainerComparisonTests.Adapters.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace IoCContainerComparison.Web.Tests.Adapters.Concerete
{
    public class UnityContainerWebWrapper : UnityContainerWrapper
    {
        protected override ITypeLifetimeManager GetPerRequestLifetimeManager()
        {
            return new PerRequestLifetimeManager();
        }
    }
}
