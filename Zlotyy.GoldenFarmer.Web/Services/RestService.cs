using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Zlotyy.GoldenFarmer.Web.Services
{
    public interface IRestService
    {
        IRestResponse<TResponse> Get<TResponse>(string endpoint) where TResponse : new();
        IRestResponse<TResponse> Get<TResponse, TPayload>(string endpoint, TPayload payload) where TResponse : new() where TPayload : class, new();
        IRestResponse<TResponse> Post<TResponse>(string endpoint, object payload) where TResponse : new();
        IRestResponse Post(string endpoint, object payload);
        IRestResponse<TResponse> Put<TResponse>(string endpoint, object payload) where TResponse : new();
        IRestResponse Put(string endpoint, object payload);
        IRestResponse<TResponse> Delete<TResponse>(string endpoint, object payload) where TResponse : new();
        IRestResponse Delete(string endpoint, object payload);
        IRestResponse<TResponse> Execute<TResponse>(Uri baseUrl, IRestRequest request) where TResponse : new();
        RestRequest CreateRequest(string endpoint, Method method, object payload = null);
    }

    public class RestService : IRestService
    {
        RestClient _client;

        public RestService(IOptions<AppSettings> options)
        {
            _client = new RestClient(options.Value.WebApi.Url)
            {
                Authenticator = new HttpBasicAuthenticator(options.Value.WebApi.UserName, options.Value.WebApi.Password)                
            };
        }

        public IRestResponse<TResponse> Get<TResponse>(string endpoint) where TResponse : new()
        {
            return Get<TResponse, object>(endpoint, null);
        }

        public IRestResponse<TResponse> Get<TResponse, TPayload>(string endpoint, TPayload payload) where TResponse : new() where TPayload : class, new()
        {
            var restRequest = new RestRequest(endpoint, Method.GET);
            restRequest.AddHeader("Content-Type", "application/json");

            if (payload != null)
            {
                foreach (var prop in payload.GetType().GetProperties())
                {
                    var value = prop.GetValue(payload, null);
                    restRequest.AddQueryParameter(prop.Name, ParameterValueFactory(value));
                }
            }

            IRestResponse<TResponse> response = _client.Execute<TResponse>(restRequest);
            return response;
        }

        public IRestResponse<TResponse> Post<TResponse>(string endpoint, object payload) where TResponse : new()
        {
            return Request<TResponse>(Method.POST, endpoint, payload);
        }

        public IRestResponse Post(string endpoint, object payload)
        {
            return Post<object>(endpoint, payload);
        }

        public IRestResponse<TResponse> Put<TResponse>(string endpoint, object payload) where TResponse : new()
        {
            return Request<TResponse>(Method.PUT, endpoint, payload);
        }

        public IRestResponse Put(string endpoint, object payload)
        {
            return Put<object>(endpoint, payload);
        }

        public IRestResponse<TResponse> Delete<TResponse>(string endpoint, object payload) where TResponse : new()
        {
            return Request<TResponse>(Method.DELETE, endpoint, payload);
        }

        public IRestResponse Delete(string endpoint, object payload)
        {
            return Delete<object>(endpoint, payload);
        }

        public IRestResponse<TResponse> Execute<TResponse>(Uri baseUrl, IRestRequest request) where TResponse : new()
        {
            _client.BaseUrl = baseUrl;
            return Execute<TResponse>(request);
        }

        public RestRequest CreateRequest(string endpoint, Method method, object payload = null)
        {
            var restRequest = new RestRequest(endpoint, method);
            if (payload != null)
            {
                restRequest.AddJsonBody(payload);
            }
            return restRequest;
        }

        private IRestResponse<TResponse> Request<TResponse>(Method method, string endpoint, object payload) where TResponse : new()
        {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("Content-Type", "application/json");
            if (payload != null)
            {
                request.AddJsonBody(payload);
            }
            IRestResponse<TResponse> response = Execute<TResponse>(request);
            return response;
        }

        private IRestResponse<TResponse> Execute<TResponse>(IRestRequest request) where TResponse : new()
        {
            IRestResponse<TResponse> result = null;
            Task.Run(async () =>
            {
                result = await _client.ExecuteAsync<TResponse>(request);
            }).Wait();

            return result;
        }

        private string ParameterValueFactory(object value)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                return date.ToString("O");
            }
            return value.ToString();
        }
    }
    public static class RestExtensions
    {
        public static RestRequest AddContent(this RestRequest request, string contentType)
        {
            request.AddHeader("Content-Type", contentType);
            return request;
        }
        public static RestRequest AddAuthorization(this RestRequest request, string tokenType, string token)
        {
            request.AddHeader("Authorization", $"{tokenType} {token}");
            return request;
        }
    }
}
