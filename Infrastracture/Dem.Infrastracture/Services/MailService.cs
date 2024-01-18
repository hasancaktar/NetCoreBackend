using Dem.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Dem.Infrastracture.Services;

public class MailService : IMailService
{
    private IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        //aşağıdaki overload'ı tetikliyor
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
    }

    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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

    public async Task<string> SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
        mail.AppendLine(_configuration["Urls:BaseUrl"]);
        mail.AppendLine("/Users/ResetPassowrd/");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br> <span style=\"font-size:12px;\">NOT: EĞER BU TALEP SİZİN TARAFINIZDAN GERÇEKLEŞTİRİLMEDİYSE BU MAIL'İ CİDDİYE ALMAYIN.</span><br>Saygılar...<br><br>DemBack AŞ");

        await SendMailAsync(to, "Şifre resetleme talebi", mail.ToString());

        return mail.ToString();

        //throw new NotImplementedException();
    }
}