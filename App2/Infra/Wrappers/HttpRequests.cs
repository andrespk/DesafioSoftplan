using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static App2.Infra.IoC.Attributes;

namespace App2.Infra.Wrappers
{
    [InjectsScoped]
    public class HttpRequests : IHttpRequests
    {
        private const int DEFAULT_TIMEOUT = 60000;
        private const int ERROR_INTERNET_CANNOT_CONNECT = -2147012867;
        private const int ERROR_WINHTTP_NO_CM_CONNECTION = -2147012816;
        private const int ERROR_INTERNET_NAME_NOT_RESOLVED = -2147012889;
        private const int ERROR_INTERNET_TIMEOUT = -2147012894;
        private const int ERROR_INTERNET_CONNECTION_ABORTED = -2147012866;
        private const int ERROR_NOT_FOUND = -2145844844;
        private const int ERROR_SERVICE_UNAVAILABLE = -2145844745;

        private readonly HttpClient _client;

        public HttpRequests()
        {
            _client = new HttpClient();
        }

        public async Task<string> ExecuteDeleteRequestAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null)
        {
            try
            {
                Uri builtUri = new Uri(targetUri, UriKind.Absolute);
                var json = JsonConvert.SerializeObject(obj);
                var request = new HttpRequestMessage
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = builtUri
                };
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                var jsonResponse = await (response.Content != null ? response.Content.ReadAsStringAsync() : null);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ExecuteGetRequestAsync(string targetUri, Action<Exception> exceptionHandler = null)
        {
            try
            {
                var builtUri = new Uri(targetUri, UriKind.Absolute);
                var response = await _client.GetAsync(builtUri);
                var jsonString = await (response.Content != null ? response.Content.ReadAsStringAsync() : null);
                return jsonString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ExecutePostRequestAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null)
        {
            try
            {
                Uri builtUri = new Uri(targetUri, UriKind.Absolute);
                var json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(builtUri, content);
                var jsonResponse = await (response.Content != null ? response.Content.ReadAsStringAsync() : null);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ExecutePutRequestAsJsonAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null)
        {
            try
            {
                Uri builtUri = new Uri(targetUri, UriKind.Absolute);
                var json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(builtUri, content);
                var jsonResponse = await (response.Content != null ? response.Content.ReadAsStringAsync() : null);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}