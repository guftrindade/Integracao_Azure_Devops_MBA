using Devops.ViewModels.Devops.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Devops.Util
{
    public static class Utility
    {
        public static string SetDefaultRepositoryName(string name, ResourceType resourceType)
        {
            var newName = name.Replace(" ", "-");
            var resourceName = GetDisplayName(resourceType);

            return string.Concat(newName, "-", resourceName).ToLower();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
