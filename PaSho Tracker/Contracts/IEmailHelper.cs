namespace PaSho_Tracker.Services;

public interface IEmailHelper 
{
    Task SendConfirmationEmail(string email, string token);
    Task SendResetPasswordEmail(string email, string token);
    Task SendEmail(string toEmail, string subject, string templateName, string urlPlaceholder, string finalUrl);
}