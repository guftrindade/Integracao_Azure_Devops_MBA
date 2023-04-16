using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Infrastructure.Request
{
    public class RequestResource
    {
        /// <summary>
        /// Nome do recurso a ser criado.
        /// </summary>
        /// <example>MeuRecurso</example>
        [Required]
        public string ResourceName { get; set; }

        /// <summary>
        /// Tipo de recurso a ser criado.
        /// </summary>
        /// <example>MeuRecurso</example>
        [Required]
        public string ResourceName { get; set; }
    }
}
