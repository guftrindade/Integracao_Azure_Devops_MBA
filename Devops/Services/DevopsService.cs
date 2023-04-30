﻿using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace Devops.Services
{
    public class DevopsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DevopsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        const string PersonalAccessToken = "dekwz3omoh3isfftycb3lelh7b5x5cmrvbrkkmqvrvfqpcpyji6q";

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
