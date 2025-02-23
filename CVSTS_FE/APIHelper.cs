﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CVSTS_FE
{
    public static class APIHelper
    {
        public static async Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PostSendEmail(this HttpClient httpClient, string url)
        {
            return await httpClient.PostAsync(url, null);
        }


        public static async Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(url, content);
        }

        public static async Task<T?> GetAsJsonAsync<T>(this HttpClient httpClient, string url)
        {
            using (var response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }

                return default;
            }
        }
        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url)
        {
            return await httpClient.DeleteAsync(url);
        }
    }
}
