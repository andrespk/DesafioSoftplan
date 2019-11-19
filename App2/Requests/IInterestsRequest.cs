using System.Threading.Tasks;

namespace App2.Requests
{
    public interface IInterestsRequest
    {
        Task<double> GetInterestRateAsync();

        Task<double> GetInterestAmountAsync(double valorInicial, int meses);

        Task<string> ShowMeTheCodeAsync();
    }
}