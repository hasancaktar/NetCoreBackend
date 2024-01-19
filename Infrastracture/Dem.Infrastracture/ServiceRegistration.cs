using Dem.Application.Abstraction;
using Dem.Application.Abstraction.Configurations;
using Dem.Application.Abstraction.Token;
using Dem.Infrastracture.Jobs;
using Dem.Infrastracture.Services;
using Dem.Infrastracture.Services.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Text;

namespace Dem.Infrastracture;

public static class ServiceRegistration
{
    public static void AddInfrastractureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenHandler, Token.TokenHandler>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IApplicationService, ApplicationService>();

        services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<LoggingBackgoundJobSetup>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Template Backend", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Description = "Başına 'Bearer' eklemeden sadece token girin",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    } });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true, //oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağı belirlediğimiz değerdir. ww.--.com
                ValidateIssuer = true, //Oluşturulacak token değerini kimin dağıttığını ifade edeceğimiz alan. www.-.com
                ValidateLifetime = true, //Oluşturulacak token değerinin süresini  kontrol edecek olan doğrılamadır
                ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır.
                ValidAudience = configuration["Token:Audience"],
                ValidIssuer = configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]))
            };
        });
    }
}