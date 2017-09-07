using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StatusQueue.Services
{
    class CustomApiAuthenticator : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("MY-APPLICATION-KEY", "037e38e0-224f-4ed6-a62f-fb87acda540f");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
