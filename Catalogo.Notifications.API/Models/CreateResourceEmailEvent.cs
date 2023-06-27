namespace Catalogo.Notifications.API.Models
{
    public class CreateResourceEmailEvent
    {
        public string ProtocolNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Description { get; set; }
    }
}
