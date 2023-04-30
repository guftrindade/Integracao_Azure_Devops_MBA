using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Infrastructure.Response
{
    public class ResponseResource
    {
        /// <summary>
        /// Estado da solicitação.
        /// </summary>
        /// <example>Em processo de aprovação</example>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Observações ou comentários sobre a solicitação.
        /// </summary>
        /// <example>Mensagem de notificação.</example>
        public string Note { get; set; }

        /// <summary>
        /// Observações ou comentários sobre a solicitação.
        /// </summary>
        /// <example>45645</example>
        public int ProtocolNumber { get; set; }
    }
}
