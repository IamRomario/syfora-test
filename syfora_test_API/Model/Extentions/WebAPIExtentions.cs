using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace syfora_test_API.Model.Extentions
{
    public static class WebAPIExtentions
    {
        public static async Task<HttpResponseMessage> SendRequestAsync(
            this HttpRequestMessage request,
            HttpClient Client
            )
        {
            var response = new HttpResponseMessage();
            try
            {
                using (Stream s = new MemoryStream())
                {
                    StreamContent copyContent = null;
                    if (request.Content != null)
                    {
                        request.Content.CopyToAsync(s).Wait();
                        s.Position = 0;
                        copyContent = new StreamContent(s);
                        foreach (var h in request.Content.Headers)
                        {
                            copyContent.Headers.Add(h.Key, h.Value);
                        }
                    }
                    return await Client.SendAsync(request);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public static HttpRequestMessage Request(string route, HttpMethod method)
        {
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(route, UriKind.Relative);
                request.Method = method == null ? HttpMethod.Get : method;
                return request;
            }
            catch (Exception ex)
            {
                //TODO:logs
            }
            return new HttpRequestMessage();
        }

        public static HttpRequestMessage UseJson(this HttpRequestMessage request, object json)
        {
            try
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                return request;
            }
            catch (Exception ex)
            {
                //TODO:logs
            }
            return request;
        }
    }
}
