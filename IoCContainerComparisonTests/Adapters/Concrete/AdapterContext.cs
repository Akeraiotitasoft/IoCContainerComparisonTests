using IoCContainerComparisonTests.Adapters.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainerComparisonTests.Adapters.Concrete
{
    public class AdapterContext : IAdapterContext
    {
        public IContainerAdapterWrapper Container { get; set; }

        public object WrappedContainer { get; set; }

        public object WrappedContext { get; set; }

        public Type FromType { get; set; }

        public Type ToType { get; set; }

        public string Name { get; set; }
    }
}
