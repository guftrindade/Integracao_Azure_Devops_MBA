using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Devops.Request
{
    public class RequestRepository
    {
        /// <summary>
        /// Nome do repositório a ser criado.
        /// </summary>
        /// <example>MyRepositoryName</example>
        [Required]
        public string Name { get; set; }

        public Project Project { get; set; }

        ///// <summary>
        ///// Tipo do repositório a ser criado.\
        ///// 1 - Backend\
        ///// 2 - Function\
        ///// 3 - Frontend
        ///// </summary>
        ///// <example>1</example>
        //[Required]
        //public ResourceType? ResourceType { get; set; }
    }

    public class Project
    {
        /// <summary>
        /// Id do projeto no Azure Devops.
        /// </summary>
        /// <example>00414692-b741-4744-9162-6098e58a5fae</example>
        [Required]
        public string Id { get; set; }
    }


}
