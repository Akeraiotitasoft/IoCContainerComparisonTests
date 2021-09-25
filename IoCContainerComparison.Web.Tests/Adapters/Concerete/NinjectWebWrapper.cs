using IoCContainerComparisonTests.Adapters.Concrete;
using Ninject.Syntax;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparison.Web.Tests.Adapters.Concerete
{
    public class NinjectWebWrapper : NinjectWrapper
    {
        protected override void CallPerRequestScope(Func<IBindingWhenInNamedWithOrOnSyntax<object>> register)
        {
            register().InRequestScope();
        }
    }
}
