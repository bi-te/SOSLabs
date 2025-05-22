using System.Runtime.Serialization;

namespace Lab2.DTOs;

[DataContract]
public class ExchangeRateRequestDto
{
    [DataMember]
    public required string FromCurrency { get; set; }
    
    [DataMember]
    public required string ToCurrency { get; set; }
}