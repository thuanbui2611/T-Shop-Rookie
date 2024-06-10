using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace T_Shop.Infrastructure.SharedServices.EmailService;
public class EmailService : IEmailService
{

    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly bool _useSSL;
    private readonly string _textParse;
    private readonly string _fromName;
    private readonly string _fromEmail;

    public EmailService(IConfiguration config)
    {
        var gmailSettings = config.GetSection("GmailSettings");
        _smtpServer = gmailSettings["SmtpServer"];
        _smtpUsername = gmailSettings["SmtpUsername"];
        _smtpPassword = gmailSettings["SmtpPassword"];
        _smtpPort = int.Parse(gmailSettings["smtpPort"]);
        _useSSL = bool.Parse(gmailSettings["UseSSL"]);
        _fromName = gmailSettings["FromName"];
        _fromEmail = gmailSettings["FromEmail"];
        _textParse = gmailSettings["TextParse"];
    }

    public async Task<bool> SendEmailAsync(SendEmailOptions emailOptions)
    {

        MimeMessage message = CreateEmail(emailOptions.ToName, emailOptions.ToEmail, emailOptions.Subject, emailOptions.Body);
        return await SendEmail(message);
    }

    private MimeMessage CreateEmail(string toName, string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_fromName, _fromEmail));
        message.To.Add(new MailboxAddress(toName, toEmail));
        message.Subject = subject;
        message.Body = new TextPart(_textParse) { Text = body };
        return message;
    }

    private async Task<bool> SendEmail(MimeMessage message)
    {
        var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_smtpServer, _smtpPort, _useSSL);
        await smtpClient.AuthenticateAsync(_smtpUsername, _smtpPassword);
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
        return true;
    }
}
