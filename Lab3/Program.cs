using GamesApi.Data;
using GamesApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddXmlSerializerFormatters();

string? connection = builder.Configuration.GetValue<string>("DbConnectionString");
builder.Services.AddDbContext<GamesDbContext>(opts => opts.UseSqlite(connection));
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GenreService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();