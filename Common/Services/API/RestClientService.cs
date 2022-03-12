using Common.Helpers;
using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Services
{
    public static class RestClientService
    {
        public static readonly string ServiceUrl = "http://apitravel.miagenciaghp.com/api";
        //public static readonly string ServiceUrl = "http://localhost:8000/api";

        public static async Task SendRequest(IEnumerable<Passenger> passengers, Cost cost, string provider, string PNR, A14FT a14FT = null)
        {
            var restClient = GetRestClient();
            var authResource = await AuthService.GetAuthResourceData(restClient);
            var request = MapperService.MapToApi(passengers, cost, provider, PNR, a14FT);

            await PostRecord(restClient, authResource, request);
        }

        private static async Task PostRecord(
            IGHPTravelportClient restClient,
            AuthResource authResource,
            ApiReservationDetailsRequest request)
        {
            request.Uid = authResource.Uid;
            await restClient.CreateReservationDetails(authResource.ToString(), request);
        }

        private static IGHPTravelportClient GetRestClient()
        {
            return RestService.For<IGHPTravelportClient>(
                new HttpClient(new ApiHandler())
                {
                    BaseAddress = new Uri(ServiceUrl)
                }
                , new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented,
                        Converters = { new StringEnumConverter() }
                    })
                }
            );
        }
    }
}
