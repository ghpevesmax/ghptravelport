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

        public static async Task SendRequest(Passenger passenger, Cost cost, Provider provider, string PNR, A14FT a14FT = null)
        {
            var restClient = GetRestClient();
            var authResource = await AuthService.GetAuthResourceData(restClient);
            var addRecordRequest = new AddRecordRequest
            {
                Titular = passenger.PassengerName,
                Pasajero = passenger.PassengerName,
                Clave = PNR,
                Proveedor = provider.ProviderName,
                Total = cost.Total,
                IVA = cost.PrimaryTaxAmount,
                IdCliente = a14FT.IdCliente,
                Concepto = a14FT.Concepto,
                CargoPorServicio = a14FT.CargoPorServicio,
                IdUsuario = a14FT.IdUsuario,
            };

            await PostRecord(restClient, authResource, addRecordRequest);
        }

        private static async Task PostRecord(
            IGHPTravelportClient restClient,
            AuthResource authResource,
            AddRecordRequest request)
        {
            request.Uid = authResource.Uid;
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
