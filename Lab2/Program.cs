using Lab2.Services;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddSingleton<IExchangeRateService, ExchangeRateService>();

WebApplication app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => {
    _ = endpoints.UseSoapEndpoint<IExchangeRateService>(opt =>
    {
        opt.Path = "/ExchangeRate.asmx";
        opt.SoapSerializer = SoapSerializer.DataContractSerializer;
        opt.CaseInsensitivePath = true;
    });
});

app.Run();