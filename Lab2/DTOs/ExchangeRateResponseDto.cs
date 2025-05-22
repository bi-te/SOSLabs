using System.Runtime.Serialization;

namespace Lab2.DTOs;

[DataContract]
public class ExchangeRateResponseDto
{
    [DataMember]
    public required decimal ExchangeRate { get; set; }
    
    [DataMember]
    public required string FromCurrency { get; set; }
    
    [DataMember]
    public required string ToCurrency { get; set; }
}