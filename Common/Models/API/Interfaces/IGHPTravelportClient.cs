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
        Task<string> Authorize([Body] string uid);

        [Get("/auth/launch/{publicIpAddress}")]
        Task<AuthResource> FirstLaunch(string publicIpAddress);
    }
}
