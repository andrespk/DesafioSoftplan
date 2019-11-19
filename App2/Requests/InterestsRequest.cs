using App2.Infra.Wrappers;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using static App2.Infra.IoC.Attributes;

namespace App2.Requests
{
    [InjectsScoped]
    public class InterestsRequest : ApiRequestAbstract, IInterestsRequest
    {
        public InterestsRequest(IHttpRequests request, IConfiguration config) : base(request, config)
        {
        }

        private async Task<double> getFloatResponseAsync(string url)
        {
            var response = await Get(url);
            return double.Parse(response,
                                System.Globalization.NumberStyles.Float,
                                Thread.CurrentThread.CurrentUICulture);
        }

        private async Task<string> getStringResponseAsync(string url)
        => await Get(url);

        public async Task<double> GetInterestAmountAsync(double valorInicial, int meses)
        {
            var url = $"{GetApiBaseUrl()}/calculaJuros?valorInicial={valorInicial}&meses={meses}";
            return await getFloatResponseAsync(url);
        }

        public async Task<double> GetInterestRateAsync()
        {
            var url = $"{GetApiBaseUrl()}/taxaJuros";
            return await getFloatResponseAsync(url);
        }

        public async Task<string> ShowMeTheCodeAsync()
        {
            var url = $"{GetApiBaseUrl()}/showMeTheCode";
            return await getStringResponseAsync(url);
        }
    }
}