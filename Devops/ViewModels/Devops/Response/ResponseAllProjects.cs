namespace Devops.ViewModels.Devops.Response
{
    public class ResponseAllProjects
    {
        public int Count { get; set; }
        public IEnumerable<Projects> Value { get; set; }
    }
}
