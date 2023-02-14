using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public class HttpClientMockHelper
    {
        private HttpClientMockHelper() { }

        public static HttpClient GetHttpClient(List<HttpClientMock> mocks)
        {
            var handler = new MockHttpMessageHandler();
            mocks.ForEach(m =>
                handler.When(HttpMethod.Get, m.Url)
                    .Respond(HttpStatusCode.OK, new StringContent(m.Response)));

            return handler.ToHttpClient();
        }
    }
}
