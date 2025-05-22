using Lab2.DTOs;

namespace Lab2.Services;

public class ExchangeRateService: IExchangeRateService
{
    private readonly Dictionary<string, Dictionary<string, decimal>> _exchangeRates = new()
    {
        { "USD", new Dictionary<string, decimal>
            {
                { "EUR", 0.93m },
                { "GBP", 0.80m },
                { "JPY", 153.33m }
            }
        },
        { "EUR", new Dictionary<string, decimal>
            {
                { "USD", 1.08m },
                { "GBP", 0.86m },
                { "JPY", 165.62m }
            }
        },
        { "GBP", new Dictionary<string, decimal>
            {
                { "USD", 1.25m },
                { "EUR", 1.16m },
                { "JPY", 191.70m }
            }
        },
        { "JPY", new Dictionary<string, decimal>
            {
                { "USD", 0.0065m },
                { "EUR", 0.0060m },
                { "GBP", 0.0052m }
            }
        }
    };
    
    public ExchangeRateResponseDto GetExchangeRate(ExchangeRateRequestDto request)
    {
        if (!_exchangeRates.TryGetValue(request.FromCurrency, out var exchangeRate))
            throw new ArgumentException($"Currency {request.FromCurrency} is not supported.");

        if (request.FromCurrency == request.ToCurrency)
        {
            return new ExchangeRateResponseDto
            {
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ExchangeRate = 1.0m
            };
        }
        
        if (!exchangeRate.ContainsKey(request.ToCurrency))
            throw new ArgumentException($"Conversion from {request.FromCurrency} to {request.ToCurrency} is not supported.");

        return new ExchangeRateResponseDto
        {
            FromCurrency = request.FromCurrency,
            ToCurrency = request.ToCurrency,
            ExchangeRate = exchangeRate[request.ToCurrency],
        };
    }

    public IEnumerable<string> GetAllCurrencies()
    {
        return _exchangeRates.Keys;
    }
}