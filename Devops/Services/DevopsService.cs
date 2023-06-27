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
using Microsoft.Extensions.Caching.Distributed;

namespace Devops.Services
{
    public class DevopsService : IDevopsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _distributedCache;

        public DevopsService(IHttpClientFactory httpClientFactory, IDistributedCache distributedCache)
        {
            _httpClientFactory = httpClientFactory;
            _distributedCache = distributedCache;
        }

        public async Task<ResponseRepository> CreateRepositoryAsync(RequestRepository request)
        {
            request.Name = SetRepositoryName(request.Name, request.ResourceType);

            var content = SetContent(request);
            var httpClient = SetHttpClient();
            var response = await httpClient.PostAsync(Constants.REPOSITORY_URI, content);

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
            var redisRepositories = await _distributedCache.GetAsync(Constants.CACHE_REPOSITORIES_KEY);

            if (redisRepositories != null)
            {
                var serializeRepository = Encoding.UTF8.GetString(redisRepositories);
                return JsonConvert.DeserializeObject<ResponseAllProjects>(serializeRepository);
            }

            var httpClient = SetHttpClient();
            var response = await httpClient.GetAsync(Constants.REPOSITORIES_URI);

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseRepository = JsonConvert.DeserializeObject<ResponseAllProjects>(responseBody);
            await SetCacheRedis(responseRepository);

            return responseRepository;
        }

        private async Task SetCacheRedis(ResponseAllProjects repositories)
        {
            var serializeRepository = JsonConvert.SerializeObject(repositories);
            var redisRepositories = Encoding.UTF8.GetBytes(serializeRepository);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _distributedCache.SetAsync(Constants.CACHE_REPOSITORIES_KEY, redisRepositories, options);
        }

        private HttpClient SetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient("DevAzureConnect");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", Constants.PAT))));
            return httpClient;
        }

        private static string SetRepositoryName(string name, ResourceType resourceType)
        {
            return Utility.SetDefaultRepositoryName(name, resourceType);
        }

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
