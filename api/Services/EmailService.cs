using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;

namespace api.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            // Get SendGrid API key from environment variables (set in Heroku)
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") 
                ?? _configuration["SendGrid:ApiKey"] 
                ?? throw new InvalidOperationException("SendGrid API Key not configured");
            
            var fromEmail = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL") 
                ?? _configuration["SendGrid:FromEmail"] 
                ?? throw new InvalidOperationException("Sender email not configured");
            
            var fromName = Environment.GetEnvironmentVariable("SENDGRID_FROM_NAME") 
                ?? _configuration["SendGrid:FromName"] 
                ?? "InvestTrack Pro";

            _logger.LogInformation($"Sending email to {to} with subject '{subject}'");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var toAddress = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, 
                      PlainTextVersion(body), body);
            
            var response = await client.SendEmailAsync(msg);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Email sent successfully to {to}");
            }
            else
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                _logger.LogWarning($"SendGrid API responded with status code {response.StatusCode}: {responseBody}");
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email: {ex.Message}");
            _logger.LogError($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    // Helper method to create a plain text version from HTML
    private string PlainTextVersion(string htmlContent)
    {
        // Very simple HTML to text conversion
        return htmlContent
            .Replace("<br>", "\n")
            .Replace("<br/>", "\n")
            .Replace("<br />", "\n")
            .Replace("<p>", "")
            .Replace("</p>", "\n\n")
            .Replace("&nbsp;", " ");
    }
}