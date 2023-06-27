namespace Catalogo.Notifications.API.Infrastructure
{
    public class CreateResourceEmailTemplate : IEmailTemplate
    {
        public CreateResourceEmailTemplate(string protocolNumber, string to, string description)
        {
            Subject = $"Solicitação de recurso - {protocolNumber} ";
            Content = $"O recurso foi solicitado com o número de protocólo - {protocolNumber}. Observação: {description}";
            To = to;
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public string To { get; set; }
    }
}
