namespace Devops.ViewModels.Devops.Response
{
    public class ResponseRepository
    {
        /// <summary>
        /// Id do repositório no Azure Devops.
        /// </summary>
        /// <example>E1868AFC-A86E-4C09-B057-C2D566D8B995</example>
        public string Id { get; set; }

        /// <summary>
        /// Nome do repositório criado no Azure Devops.
        /// </summary>
        /// <example>MyRepositoryName</example>
        public string Name { get; set; }

        /// <summary>
        /// Url do repositório criado no Azure Devops.
        /// </summary>
        /// <example>"https://dev.azure.com/MyResource/MyProjectName/_apis/git/repositories/E1868AFC-A86E-4C09-B057-C2D566D8B995"</example>
        public string Url { get; set; }

        /// <summary>
        /// Url para clonar repositório do Azure Devops.
        /// </summary>
        /// <example>"https://dev.azure.com/MyResource/MyProjectName/_git/MyRepositoryName"</example>
        public string RemoteUrl { get; set; }
    }
}
