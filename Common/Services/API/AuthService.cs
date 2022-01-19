using Common.Helpers;
using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public static class AuthService
    {
        public static async Task<AuthResource> GetAuthResourceData(IGHPTravelportClient restclient)
        {
            AuthResource authResource;
            if (!FileHelper.AuthFileExists())
            {
                authResource = await SetFirstLaunchData(restclient);
            }
            else
            {
                authResource = FileHelper.GetAuthResources();
                if (authResource == null || (!authResource.HasUid && !authResource.HasToken))
                {
                    authResource = await SetFirstLaunchData(restclient);
                }
                else if (authResource.HasUid && !authResource.HasToken)
                {

                    authResource.Token = await GetAuthToken(restclient, authResource.Uid);
                    await FileHelper.SetAuthResourceAsync(authResource);
                }
            }
            return authResource;
        }

        private static async Task<AuthResource> SetFirstLaunchData(IGHPTravelportClient restclient)
        {
            var authResource = await GetFirstLaunchToken(restclient);
            await FileHelper.SetAuthResourceAsync(authResource);
            return authResource;
        }
        private static async Task<AuthResource> GetFirstLaunchToken(IGHPTravelportClient restclient)
        {
            var publicIpAddress = await NetworkHelper.GetPublicIpAddress();
            var authResource = await restclient.FirstLaunch(publicIpAddress);
            return authResource;
        }
        private static async Task<string> GetAuthToken(IGHPTravelportClient restclient, string uid)
        {
            var request = new AuthorizeTokenRequest { Uid = uid };
            var response = await restclient.Authorize(request);
            return response.Token;
        }

    }
}
