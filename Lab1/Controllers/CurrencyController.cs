using Lab1.DTO;
using Lab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyConverterService _currencyConverterService;

    public CurrencyController(CurrencyConverterService currencyConverterService)
    {
        _currencyConverterService = currencyConverterService;
    }

    [HttpPost("convert")]
    public ActionResult<ConversionResponseDto> Convert(ConversionRequestDto request)
    {
        try
        {
            var result = _currencyConverterService.Convert(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("supported-currencies")]
    public ActionResult<IEnumerable<string>> GetSupportedCurrencies()
    {
        return Ok(_currencyConverterService.GetAllCurrencies());
    }
}