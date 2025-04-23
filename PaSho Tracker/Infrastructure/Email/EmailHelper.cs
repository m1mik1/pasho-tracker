using System.Net;
using System.Net.Mail;

namespace PaSho_Tracker.Services.Email;

public class EmailHelper : IEmailHelper
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailHelper> _logger;

    public EmailHelper(IConfiguration configuration, ILogger<EmailHelper> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmail(string toEmail, string subject, string templateName, string urlPlaceholder, string finalUrl)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(),"Resources" ,"EmailTemplates", templateName);

        if (!File.Exists(templatePath))
        {
            _logger.LogError("Email template not found: {Path}", templatePath);
            throw new FileNotFoundException("Email template not found", templatePath);
        }

        string htmlBody = File.ReadAllText(templatePath).Replace(urlPlaceholder, finalUrl);

        var message = new MailMessage(_configuration["Email:Sender"], toEmail, subject, htmlBody)
        {
            IsBodyHtml = true
        };

        var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
        {
            Port = int.Parse(_configuration["Email:Port"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _configuration["Email:Sender"],
                _configuration["Email:Password"]
            ),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        await smtpClient.SendMailAsync(message);
        _logger.LogInformation("Email sent to {Email} with subject: {Subject}", toEmail, subject);
    }

    public async Task SendConfirmationEmail(string email, string token)
    {
        var baseUrl = _configuration["Frontend:BaseUrl"] ?? "http://localhost:3000";
        var confirmUrl = $"{baseUrl}/confirm-email?email={email}&token={Uri.EscapeDataString(token)}";

        await SendEmail(
            email,
            "Confirm your email",
            "ConfirmEmailTemplate.html",
            "{{CONFIRM_URL}}",
            confirmUrl
        );
    }

    public async Task SendResetPasswordEmail(string email, string token)
    {
        var baseUrl = _configuration["Frontend:BaseUrl"] ?? "http://localhost:3000";
        var resetUrl = $"{baseUrl}/reset-password?email={email}&token={Uri.EscapeDataString(token)}";

        await SendEmail(
            email,
            "Reset your password",
            "ResetPasswordTemplate.html",
            "{{RESET_URL}}",
            resetUrl
        );
    }

    
}
