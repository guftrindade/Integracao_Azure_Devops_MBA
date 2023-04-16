using System.ComponentModel.DataAnnotations;

namespace Devops.ViewModels.Devops.Enums
{
    public enum ResourceType
    {
        [Display(Name = "api")]
        BACKEND = 1,
        [Display(Name = "fn")]
        FUNCTION = 2,
        [Display(Name = "web")]
        FRONTEND = 3
    }
}
