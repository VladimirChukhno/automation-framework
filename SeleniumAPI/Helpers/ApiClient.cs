using Newtonsoft.Json;
using RestSharp;
using SeleniumAPI.Config;
using SeleniumAPI.Models.Client;

namespace SeleniumAPI.Helpers
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient()
        {
            _client = new RestClient(SeleniumAPI.Config.Config.BaseUrl);
        }

        // Нормальний POST
        public RestResponse PostClient(ClientRequest request)
        {
            var restRequest = new RestRequest("/client", Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(request);

            return _client.Execute(restRequest);
        }

        // Неправильний HTTP метод
        public RestResponse ExecuteWrongMethod(string endpoint, Method method)
        {
            var request = new RestRequest(endpoint, method);
            return _client.Execute(request);
        }

        // Неправильний Content-Type
        public RestResponse ExecuteInvalidFormat(string endpoint, string contentType)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Content-Type", contentType);
            request.AddBody("<xml></xml>");

            return _client.Execute(request);
        }

        // Невалідний JSON
        public RestResponse ExecuteInvalidJson(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddStringBody("{ invalid json }", DataFormat.Json);

            return _client.Execute(request);
        }

        // ✅ Deserialize
        public T DeserializeResponse<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        // ✅ Send other raw requests
        public RestResponse ExecuteRaw(RestRequest request)
        {
            return _client.Execute(request);
        }
    }
}