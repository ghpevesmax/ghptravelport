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
        Task PostRecord([Header("Authorization")] string auth, [Body] AddRecordRequest request);

        [Post("/auth")]
        Task<AuthResponse> Authorize([Body(buffered: true)] UidModel request);

        [Get("/auth/launch/{publicIpAddress}")]
        Task<AuthResource> FirstLaunch(string publicIpAddress);
    }
}
