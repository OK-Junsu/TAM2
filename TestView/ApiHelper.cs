using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TestView
{
    public class ApiHelper
    {
        public HttpClient Client { get; init; }
        public string Uri { get; init; }
        public string ApiRoot { get; init; }
        public string FullPath { get; init; }


        public ApiHelper(string uri, string apiRoot)
        {
            Client = GetHttpClient(uri);
            Uri = RemoveLastSlash(uri);
            ApiRoot = RemoveLastSlash(apiRoot);
            FullPath = $"{Uri}/{ApiRoot}";
        }


        private static HttpClient GetHttpClient(string uri)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static string RemoveLastSlash(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (url.EndsWith('/'))
                    return url.Remove(url.Length - 1, 1);
                else
                    return url;
            }
            else
                throw new ArgumentException("Path is Not Valid");
        }

        public async Task<T?> Get<T>()
        {
            var response = await Client.GetAsync(FullPath);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<T>() : default;
        }
        public async Task<T?> Get<T>(int id)
        {
            var response = await Client.GetAsync($"{FullPath}/{id}");
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<T>() : default;
        }
        public async Task Create<T>(T data)
        {
            var response = await Client.PostAsJsonAsync(ApiRoot, data);
            response.EnsureSuccessStatusCode();
        }
        public async Task Update<T>(T data, int id)
        {
            var response = await Client.PutAsJsonAsync($"{ApiRoot}/{id}", data);
            response.EnsureSuccessStatusCode();
        }
        public async Task Delete(int id)
        {
            await Client.DeleteAsync($"{ApiRoot}/{id}");
        }
    }
}
