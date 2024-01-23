using Dem.Application.Middlewares.Exception;
using Dem.Infrastracture;
using Dem.Persistance;
using Dem.WebApi.CustomAttributes;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastractureServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();

builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    option.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");

    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US")
    };

    option.SupportedCultures = supportedCultures;
    option.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRequestLocalization();

app.MapControllers();

app.Run();