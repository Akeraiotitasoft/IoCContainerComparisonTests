using Akeraiotitasoft.ExodusContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Mock.Concrete.Exodus
{
    public class CustomScopeGovernor : IScopeGovernor
    {
        public object GetScope(IExodusContainer exodusContainer, IExodusContainerScope exodusContainerScope)
        {
            throw new NotImplementedException();
        }
    }
}
