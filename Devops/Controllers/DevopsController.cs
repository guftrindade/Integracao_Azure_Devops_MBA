using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;
using Newtonsoft.Json;

namespace Devops.Controllers
{
    public class DevopsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DevopsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        const string PersonalAccessToken = "dekwz3omoh3isfftycb3lelh7b5x5cmrvbrkkmqvrvfqpcpyji6q";

        [HttpGet("projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAllProjects>> GetAllProjectsAsync()
        {
            var response = await ServiceGetAllProjectsAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("repository")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseResource>> CreateRepositoryAsync([FromBody] RequestRepository request)
        {
            var response = await ServiceCreateRepositoryAsync(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Created(nameof(GetAllProjectsAsync), response);
        }

        public async Task<ResponseResource> ServiceCreateRepositoryAsync(RequestRepository request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string requestUri = "https://dev.azure.com/catalogomba/_apis/git/repositories?api-version=7.0";
            var httpClient = SetHttpClient();
            var response = await httpClient.PostAsync(requestUri, content);

            if (!response.StatusCode.Equals(HttpStatusCode.Created))
            {
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseResource>(responseBody);
        }

        public async Task<ResponseAllProjects> ServiceGetAllProjectsAsync()
        {
            var httpClient = SetHttpClient();
            string requestUri = "https://dev.azure.com/catalogomba/00414692-b741-4744-9162-6098e58a5fae/_apis/git/repositories";
            var response = await httpClient.GetAsync(requestUri);

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseAllProjects>(responseBody);
        }

        private HttpClient SetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient("DevAzureConnect");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", PersonalAccessToken))));
            return httpClient;
        }
    }
}
