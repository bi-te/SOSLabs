using System.ServiceModel;
using Lab2.DTOs;

namespace Lab2.Services;

[ServiceContract]
public interface IExchangeRateService
{
    [OperationContract]
    ExchangeRateResponseDto GetExchangeRate(ExchangeRateRequestDto request);

    [OperationContract]
    IEnumerable<string> GetAllCurrencies();
}