using Refit;
using System.Threading.Tasks;

namespace Common.Models
{
    [Headers(
        "Accept: application/json",
        "User-Agent: GHPTravelPortFileToRest"
    )]
    public interface IGHPTravelportClient
    {
        [Post("/records/store")]
        Task CreateReservationDetails([Header("Authorization")] string auth, [Body(buffered: true)] ApiReservationDetailsRequest request);

        [Post("/auth")]
        Task<AuthorizeTokenResponse> Authorize([Body(buffered: true)] AuthorizeTokenRequest request);

        [Get("/auth/launch/{publicIpAddress}")]
        Task<AuthResource> FirstLaunch(string publicIpAddress);
    }
}
