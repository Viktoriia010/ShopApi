using Shop.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string email, string subject ,string body, CancellationToken cancellationToken)
    {
        string fromEmail = "koshvikt10@gmail.com";
        string password = "jair ljjt rays fvot";

        using var message = new MailMessage();

        message.From = new MailAddress(fromEmail);
        message.To.Add(email);

        message.Subject = subject;

        message.Body = $"""
            {body}
            """;

        using var smtp = new SmtpClient("smtp.gmail.com", 587);

        smtp.Credentials = new NetworkCredential(
            fromEmail,
            password);

        smtp.EnableSsl = true;

        await smtp.SendMailAsync(message, cancellationToken);
    }
    //public async Task SendPasswordResetEmailAsync(string email, string link)
    //{
    //    string fromEmail = "koshvikt10@gmail.com";
    //    string password = "jair ljjt rays fvot";

    //    using var message = new MailMessage();

    //    message.From = new MailAddress(fromEmail);
    //    message.To.Add(email);

    //    message.Subject = "Встановлення пароля";

    //    message.Body = $"""
    //        Вас було додано до системи як адміністратора/модератора.

    //        Для встановлення пароля перейдіть за посиланням:

    //        {link}

    //        Посилання дійсне протягом 30 хвилин.
    //        """;

    //    using var smtp = new SmtpClient("smtp.gmail.com", 587);

    //    smtp.Credentials = new NetworkCredential(
    //        fromEmail,
    //        password);

    //    smtp.EnableSsl = true;

    //    await smtp.SendMailAsync(message);
    //}

}
