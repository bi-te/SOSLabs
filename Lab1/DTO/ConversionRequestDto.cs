using System.Xml.Serialization;

namespace Lab1.DTO;

[XmlRoot("ConversionRequest")]
public class ConversionRequestDto
{
    public decimal Amount { get; set; }
    public string FromCurrency { get; set; } = string.Empty;
    public string ToCurrency { get; set; } = string.Empty;
}