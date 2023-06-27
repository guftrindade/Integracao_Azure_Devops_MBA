using SendGrid.Helpers.Mail;
using SendGrid;

namespace Catalogo.Notifications.API.Infrastructure
{
    public class EmailService : INotificationService
    {
        private readonly ISendGridClient _client;
        private readonly string? _from;
        private readonly string? _name;

        public EmailService(IConfiguration configuration, ISendGridClient client)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException();
            }

            _client = client;
            _from = configuration.GetSection("SendGrid:From").Value;
            _name = configuration.GetSection("SendGrid:Name").Value;
        }

        public async Task<Response> Send(IEmailTemplate template)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_from, _name),
                Subject = template.Subject,
                PlainTextContent = template.Content
            };

            message.AddTo(template.To);

            return await _client.SendEmailAsync(message);
        }
    }
}
