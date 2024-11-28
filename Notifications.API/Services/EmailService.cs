
using System.Net.Mail;
using System.Net;

namespace Notifications.API.Services
{
    public class EmailService : IEmailService
    {
        public readonly SmtpSettings settings;
        //private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration configuration)
        {
            //configuration.GetSection("Smtp").Bind(settings);
            settings = new SmtpSettings()
            {
                FromAddress = "elat.aymerick@gmail.com",
                Password = "Emerick@101290",
                Port = 465,
                Server = "smtp.gmail.com",
                UserName = "elat.aymerick@gmail.com"
            };
            //logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(NotificationMail mail)
        {
            if (mail.Email is null) {
                return false;
            }
            // Create a new MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(settings.FromAddress);
            mailMessage.To.Add(mail.Email);
            mailMessage.Subject = mail.Subject;
            mailMessage.Body = mail.Body;

            // Configure the SMTP client
            SmtpClient smtpClient = new SmtpClient(settings.Server, settings.Port);
            smtpClient.Port = settings.Port;
            smtpClient.Credentials = new NetworkCredential(settings.FromAddress, settings.Password);
            smtpClient.EnableSsl = true;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                //logger.LogWarning("Error: " + ex.Message);
                
                return false;
            }
        }
    }
}
