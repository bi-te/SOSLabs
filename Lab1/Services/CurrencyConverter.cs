using Lab1.DTO;
namespace Lab1.Services;

public class CurrencyConverterService
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

    public List<string> GetAllCurrencies()
    {
        return _exchangeRates.Keys.ToList();
    }
    
    public ConversionResponseDto Convert(ConversionRequestDto request)
    {
        if (!_exchangeRates.TryGetValue(request.FromCurrency, out var exchangeRate))
            throw new ArgumentException($"Currency {request.FromCurrency} is not supported.");

        if (request.FromCurrency == request.ToCurrency)
        {
            return new ConversionResponseDto
            {
                Amount = request.Amount,
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ExchangeRate = 1.0m,
                ResultAmount = request.Amount
            };
        }
        
        if (!exchangeRate.ContainsKey(request.ToCurrency))
            throw new ArgumentException($"Conversion from {request.FromCurrency} to {request.ToCurrency} is not supported.");

        decimal rate = exchangeRate[request.ToCurrency];
        
        decimal resultAmount = request.Amount * rate;

        return new ConversionResponseDto
        {
            Amount = request.Amount,
            FromCurrency = request.FromCurrency,
            ToCurrency = request.ToCurrency,
            ExchangeRate = rate,
            ResultAmount = resultAmount
        };
    }
}