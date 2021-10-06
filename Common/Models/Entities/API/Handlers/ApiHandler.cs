using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Models.Entities
{
    class ApiHandler : DelegatingHandler
    {
        public ApiHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }
    }

}
