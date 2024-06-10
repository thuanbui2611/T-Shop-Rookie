namespace T_Shop.Infrastructure.SharedServices.EmailService;
public interface IEmailService
{
    Task<bool> SendEmailAsync(SendEmailOptions emailOptions);
}
