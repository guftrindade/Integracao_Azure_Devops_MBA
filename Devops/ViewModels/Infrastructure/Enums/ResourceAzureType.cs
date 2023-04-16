using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Infrastructure.Enums
{
    public enum ResourceAzureType
    {
        [Display(Name = "web_api")]
        WEB_APP = 1,

        [Display(Name = "sql_database")]
        SQL_DATABASE = 2,

        [Display(Name = "function_app")]
        FUNCTION_APP = 3
    }
}
