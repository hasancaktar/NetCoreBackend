using Dem.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Dem.Infrastracture.Services;

public class MailService : IMailService
{
    IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        //aşağıdaki overload'ı tetikliyor
        await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
    }

    public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {

        MailMessage mail = new();
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = isBodyHtml;
        foreach (var to in tos)
            mail.To.Add(to);
        mail.From = new(_configuration["Mail:MailAddress"], _configuration["Mail:DisplayName"], Encoding.UTF8);
        SmtpClient smtp = new();
        smtp.Credentials = new NetworkCredential(_configuration["Mail:MailAddress"], _configuration["Mail:MailPassword"]);
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Host = _configuration["Mail:Host"];
        smtp.UseDefaultCredentials = false;

        await smtp.SendMailAsync(mail);
    }
}
