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
        //public static readonly string ServiceUrl = "http://127.0.0.1:8000/api";

        public static async Task<bool> SendRequest(Passenger passenger, Cost cost, Provider provider, string PNR)
        {
            try
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
            catch (ApiException ex)
            {
                return false;
            }
            return true;
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

            try
            {
                await api.PostRecord(request);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
