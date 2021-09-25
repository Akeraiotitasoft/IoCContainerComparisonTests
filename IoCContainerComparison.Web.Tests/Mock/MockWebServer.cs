using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;


namespace IoCContainerComparisonTests.Mock
{
    public class MockWebServer
    {
        public static HttpContext GetMockHttpContext()
        {
            HttpRequest request = new HttpRequest("test", "http://myurl", "myParameters=1");
            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            HttpResponse response = new HttpResponse(textWriter);
            HttpContext context = new HttpContext(request, response);
            CallContext.HostContext = context;
            context.ApplicationInstance = new HttpApplication();

            return context;
        }

        public static HttpApplication GetHttpApplication()
        {
            Moq.Mock<HttpApplication> mockHttpApplication = new Moq.Mock<HttpApplication>();
            return (HttpApplication)mockHttpApplication.Object;
        }

        public static HttpContext GetHttpContext()
        {
            Moq.Mock<HttpContext> mockHttpApplication = new Moq.Mock<HttpContext>();
            return (HttpContext)mockHttpApplication.Object;
        }

        public static HttpContext GetHttpContextWithSideEffect()
        {
            HttpContext context = GetHttpContext();
            typeof(HttpContext).GetProperty("Current").SetValue(null,context);
            return context;
        }
    }
}
