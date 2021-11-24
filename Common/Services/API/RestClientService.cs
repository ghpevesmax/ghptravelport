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
    public static class RestClientService
    {
        public static readonly string ServiceUrl = "http://apitravel.miagenciaghp.com/api";
        //public static readonly string ServiceUrl = "http://localhost:8000/api";

        public static async Task SendRequest(Passenger passenger, Cost cost, Provider provider, string PNR)
        {
            var restClient = GetRestClient();
            var authResource = await AuthService.GetAuthResourceData(restClient);
            await PostRecord(restClient, authResource, new AddRecordRequest
            {
                Titular = passenger.PassengerName,
                Cliente = passenger.PassengerName,
                Clave = PNR,
                Proveedor = provider.ProviderName,
                Total = cost.Total,
                IVA = cost.PrimaryTaxAmount
            });
        }

        private static async Task PostRecord(
            IGHPTravelportClient restClient,
            AuthResource authResource,
            AddRecordRequest request)
        {
            request.UID = authResource.Uid;
            await restClient.PostRecord(authResource.ToString(), request);
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
