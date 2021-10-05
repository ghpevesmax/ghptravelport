using Common.Models.Entities;
using Common.Services.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Services
{
    public static class RestClientService
    {
        public static readonly string ServiceUrl = "http://apitravel.miagenciaghp.com/api";

        public static async Task SendRequest(Passenger passenger, Cost cost, Provider provider, string PNR)
        {
            await PostRecords(new AddRecordRequest
            {
                Titular = passenger.PassengerName,
                Cliente = passenger.PassengerName,
                Clave = PNR,
                Proveedor = provider.ProviderName,
                Total = cost.Total,
                IVA = cost.PrimaryTaxAmount
            });
        }

        private static async Task PostRecords(AddRecordRequest request)
        {
            var api = RestService.For<IGHPTravelportClient>(
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
            await api.PostRecord(request);
        }
    }
}
