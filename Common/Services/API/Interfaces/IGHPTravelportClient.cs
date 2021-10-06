using Common.Models.Entities;
using Refit;
using System.Threading.Tasks;

namespace Common.Services.API.Models
{
    [Headers(
        "Content-Type: application/json"
        ,"User-Agent: GHPTravelPortFileToRest"
    )]
    public interface IGHPTravelportClient
    {
        [Post("/records/store")]
        Task PostRecord([Body(buffered: true)] AddRecordRequest request);
    }
}
