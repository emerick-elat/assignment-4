namespace Notifications.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(NotificationMail mail);
    }
}
