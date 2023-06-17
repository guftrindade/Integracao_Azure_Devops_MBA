using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Devops.Util;
using Devops.ViewModels.Devops.Enums;
using Devops.Services.Interfaces;
using System.Text.Json;

namespace Devops.Services
{
    public class DevopsService : IDevopsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DevopsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseRepository> CreateRepositoryAsync(RequestRepository request)
        {
            request.Name = SetRepositoryName(request.Name, request.ResourceType);
            
            var content = SetContent(request);
            var httpClient = SetHttpClient();
            var response = await httpClient.PostAsync(RepositoryUri, content);

            if (!TratarErrosResponse(response))
            {
                return new ResponseRepository
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            var responseResource = await DeserializarObjetoResponse<ResponseRepository>(response);
            SaveInDataBase(responseResource);

            return responseResource;
        }

        private void SaveInDataBase(ResponseRepository response)
        {
            
        }
        private static StringContent SetContent(RequestRepository request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<ResponseAllProjects> GetAllProjectsAsync()
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

        private static string SetRepositoryName(string name, ResourceType resourceType)
        {
            var newName =  Utility.SetDefaultRepositoryName(name, resourceType);
            return newName;
        }

        private const string PersonalAccessToken = "dekwz3omoh3isfftycb3lelh7b5x5cmrvbrkkmqvrvfqpcpyji6q";
        private const string RepositoryUri = "https://dev.azure.com/catalogomba/_apis/git/repositories?api-version=7.0";

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }


        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public class CustomHttpRequestException : Exception
        {
            public HttpStatusCode StatusCode;

            public CustomHttpRequestException() { }

            public CustomHttpRequestException(string message, Exception innerException)
                : base(message, innerException) { }

            public CustomHttpRequestException(HttpStatusCode statusCode)
            {
                StatusCode = statusCode;
            }
        }
    }
}
