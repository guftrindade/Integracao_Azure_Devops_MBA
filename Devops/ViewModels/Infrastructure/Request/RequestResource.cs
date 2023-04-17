using Devops.ViewModels.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Infrastructure.Request
{
    public class RequestResource
    {
        /// <summary>
        /// Sugestão de nome do recurso a ser criado no Portal do Azure.
        /// </summary>
        /// <example>MeuRecurso</example>
        [Required]
        public string ResourceName { get; set; }

        /// <summary>
        /// Tipo de recurso a ser criado.\
        /// 1 - Web App\
        /// 2 - SQL Database\
        /// 3 - Function App
        /// </summary>
        /// <example>1</example>
        [Required]
        public ResourceAzureType? ResourceAzureType { get; set; }

        /// <summary>
        /// Observações ou comentários sobre a solicitação.
        /// </summary>
        /// <example>Favor criar recurso em servidor localizado no Brasil.</example>
        public string Note { get; set; }

        /// <summary>
        /// Identificação do desenvolvedor.
        /// </summary>
        /// <example>gustavotrindade@dev.com</example>
        public string Email { get; set; }
    }
}
