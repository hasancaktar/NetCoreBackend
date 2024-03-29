﻿namespace Dem.Application.Abstraction;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);

    Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);

    Task<string> SendPasswordResetMailAsync(string to, string userId, string resetToken);
}