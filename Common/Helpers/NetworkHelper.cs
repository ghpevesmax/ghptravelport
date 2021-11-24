using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class NetworkHelper
    {
        public static async Task<string> GetPublicIpAddress()
        {
            var publicIpAddress = await new HttpClient().GetStringAsync("https://api.ipify.org");
            return publicIpAddress;
        }

    }
}
