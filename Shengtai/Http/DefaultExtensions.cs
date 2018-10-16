using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Http
{
    public static class DefaultExtensions
    {
        public static async Task<T> Post<T>(string requestUri, object value)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(value);
                    using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage message = await client.PostAsync(requestUri, stringContent);
                        if (message != null)
                        {
                            if (message.IsSuccessStatusCode)
                            {
                                var result = await message.Content.ReadAsStringAsync();

                                return JsonConvert.DeserializeObject<T>(result,
                                    new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                            }
                        }
                    }
                }
            }

            return default(T);
        }

        public static string HttpPost(string requestUriString, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest request = WebRequest.Create(requestUriString) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST";
            request.ContentType = "application/json";

            var s = JsonConvert.SerializeObject(value);
            byte[] buffer = Encoding.UTF8.GetBytes(s);

            Stream stream = request.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            string result = reader.ReadToEnd();
            return result;
        }
    }
}
