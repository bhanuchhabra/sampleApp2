using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SalaryWebApp.Services
{
    public class WebApiCallerService
    {
        private readonly Uri baseUrl = new Uri("http://localhost:44933/");
        public async Task<string> WebApiCallerGet(string url)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseUrl;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    result = await apiResponse.Content.ReadAsStringAsync();

                }
            }
            return result;
        }
        public async Task<bool> WebApiCallerPost<T>(string url, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseUrl;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage apiResponse = await client.PostAsJsonAsync(url, data);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var postResult = await apiResponse.Content.ReadAsStringAsync();
                    return Convert.ToBoolean(Convert.ToInt32(postResult));
                }
            }
            return false;
        }
    }
}