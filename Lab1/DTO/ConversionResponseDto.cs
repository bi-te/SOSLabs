using System.Xml.Serialization;

namespace Lab1.DTO;

[XmlRoot("ConversionResponse")]
public class ConversionResponseDto
{
    public decimal Amount { get; set; }
    public string FromCurrency { get; set; } = string.Empty;
    public string ToCurrency { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
    public decimal ResultAmount { get; set; }
}