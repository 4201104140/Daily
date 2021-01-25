using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Http
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                "EAAv7fxYGqlIBADeAuWH5ja3BOjuS9H01QmTFofg0BY4OEhne97ANCMDT5MlZAruzEiZANT4gYKZAMKSnflWFY9hmBJGLMqzuYcsMmSYBtZCTLwknyw612vIZASo5i34vLib1l5fRM6A5y6kznh9uVUipcdqZAu223l1dutgwLJk4eYMBgYsSpoCo5IEZCboWZAfHp6rhkcS2fgZDZD");
            string url = "https://graph.facebook.com/v9.0/me?fields=id,name";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                JsonDocument json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var id = json.RootElement.GetProperty("id");
                var name = json.RootElement.GetProperty("name");
            }
        }
    }
}
