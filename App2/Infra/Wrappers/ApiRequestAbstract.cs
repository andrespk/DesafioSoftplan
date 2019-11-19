using App2.Infra.Wrappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace App2.Infra.Wrappers
{
    public class ApiRequestAbstract
    {
        private readonly IHttpRequests _request;
        private readonly IConfiguration _config;

        public ApiRequestAbstract(IHttpRequests request, IConfiguration config)
        {
            _request = request;
            _config = config;
        }

        public string GetApiBaseUrl()
        => _config.GetValue<string>("ApiBaseUrl");

        public async Task<string> Get(string url)
        => await _request.ExecuteGetRequestAsync(url);

        public async Task<string> Post(string url, object obj)
        => await _request.ExecutePostRequestAsync(url, obj);

        public async Task<string> Delete(string url, object obj)
        => await _request.ExecuteDeleteRequestAsync(url, obj);

        public async Task<string> Put(string url, object obj)
        => await _request.ExecutePutRequestAsJsonAsync(url, obj);
    }
}