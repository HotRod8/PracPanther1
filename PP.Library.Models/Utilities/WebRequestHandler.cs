using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PP_Library.Utilities
{
    public class WebRequestHandler
    {
        //To get this working on an iOS app from one of my Macbooks
        //or vice versa, use my IPD4 address instead of localhost
        private string host = "localhost";
        private string port = "5074";
        private HttpClient Client { get; }
        public WebRequestHandler()
        {
            Client = new HttpClient();
        }

        //diff. b/tw https and http:
        //https requires a certificate to use here
        //If it's invalid, you'll get a 401 error on your response.
        //  1. Get a new certificate(non-trivial)
        //  2. Replace all https w/ http, go to server launch properties
        //  (play button dropdown), and go to https to eliminate the https url
        public async Task<string> Get(string url)
        {
            var fullUrl = $"http://{host}:{port}{url}";
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client
                        .GetStringAsync(fullUrl)
                        .ConfigureAwait(false);
                    return response;
                }
            } catch(Exception)
            {
                return null;
            }

        }

        public async Task<string> Post(string url, object obj)
        {
            var fullUrl = $"http://{host}:{port}{url}";
            using (var client = new HttpClient())
            {
                using(var request = new HttpRequestMessage(HttpMethod.Post, fullUrl))
                {
                    var json = JsonConvert.SerializeObject(obj);
                    using(var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using(var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            if(response.IsSuccessStatusCode)
                            {
                                return await response.Content.ReadAsStringAsync();
                            }
                            return "ERROR";
                        }
                    }
                }
            }
        }

        public async Task<string> Delete(string url) 
        {
            var fullUrl = $"http://{host}:{port}{url}";
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl))
                {
                    using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        return "ERROR";
                    }
                }
            }
        }
    }
}
