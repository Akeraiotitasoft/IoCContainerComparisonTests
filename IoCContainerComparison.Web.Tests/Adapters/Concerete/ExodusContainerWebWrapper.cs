using Akeraiotitasoft.ExodusContainer;
using Akeraiotitasoft.ExodusContainer.Web.Governor;
using IoCContainerComparisonTests.Adapters.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparison.Web.Tests.Adapters.Concerete
{
    public class ExodusContainerWebWrapper : ExodusContainerWrapper
    {
        protected override IScopeGovernor CreatePerRequestScopeGovernor()
        {
            return new PreRequestGovernor();
        }
    }
}
