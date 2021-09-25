using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Adapters.Enumeration
{
    public enum AdapterScope
    {
        Singleton,
        Transient,
        PerThread,
        PerRequest,
        Scoped,
        Custom
    }
}
