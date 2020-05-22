using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkMarketWebAPI.Models
{

    public class HttpClientHelper
    {
        string BaseUrl;
        public string ContentType;
        public string AccessToken { get; set; }


        public HttpClientHelper(string baseUrl, string contentType = "application/json", string accessToken = null)
        {
            BaseUrl = baseUrl;
            ContentType = contentType;
            AccessToken = accessToken;
        }

        public T Post<T, V>(V model, String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string serilized = JsonConvert.SerializeObject(model);
            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(serilized, Encoding.UTF8, ContentType)
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = client.PostAsync(serviceUrl, inputMessage.Content).Result;
            if (message.IsSuccessStatusCode)
            {
                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                throw new Exception(message.ReasonPhrase);
            }
            return returnObject;
        }

        public T Put<T, V>(V model, String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string serilized = JsonConvert.SerializeObject(model);

            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(serilized, Encoding.UTF8, ContentType)
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = client.PutAsync(serviceUrl, inputMessage.Content).Result;
            if (message.IsSuccessStatusCode)
            {
                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                throw new Exception(message.ReasonPhrase);
            }
            return returnObject;
        }

        public T Get<T>(String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = client.GetAsync(serviceUrl).Result;
            if (message.IsSuccessStatusCode)
            {

                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            return returnObject;
        }

        public T Delete<T>(String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = client.DeleteAsync(serviceUrl).Result;
            if (message.IsSuccessStatusCode)
            {
                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            return returnObject;
        }
        public async Task<T> PostAsync<T, V>(V model, String serviceUrl) where T : ResponseCommon, new()
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string serilized = model.GetType() == typeof(string) ? model as string : JsonConvert.SerializeObject(model);
            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(serilized, Encoding.UTF8, ContentType)
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = await client.PostAsync(serviceUrl, inputMessage.Content);
            if (message.IsSuccessStatusCode)
            {
                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            return returnObject;
        }


        public async Task<T> PostAsync<T>(String serviceUrl) where T : ResponseCommon, new()
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent("", Encoding.UTF8, ContentType)
            };

            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

            }

            HttpResponseMessage message = await client.PostAsync(serviceUrl, inputMessage.Content);
            if (message.IsSuccessStatusCode)
            {
                string output = message.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                try
                {
                    string output = await message.Content.ReadAsStringAsync();
                    returnObject = JsonConvert.DeserializeObject<T>(output);
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }


            return returnObject;
        }

        public async Task<T> PutAsync<T, V>(V model, String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string serilized = JsonConvert.SerializeObject(model);
            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(serilized, Encoding.UTF8, ContentType)
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = await client.PutAsync(serviceUrl, inputMessage.Content);
            if (message.IsSuccessStatusCode)
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                throw new Exception(message.ReasonPhrase);
            }
            return returnObject;
        }

        public async Task<T> GetAsync<T>(String serviceUrl) where T : ResponseCommon, new()
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = await client.GetAsync(serviceUrl);

            if (message.IsSuccessStatusCode)
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = JsonConvert.DeserializeObject<T>(output);

                return returnObject;
            }
            return returnObject;
        }

        public async Task<KeyValuePair<T1, List<T2>>> GetListAsync<T1, T2>(String serviceUrl) where T1 : ResponseCommon, new() where T2 : class
        {
            KeyValuePair<T1, List<T2>> returnObject = new KeyValuePair<T1, List<T2>>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = await client.GetAsync(serviceUrl);

            if (message.IsSuccessStatusCode)
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = new KeyValuePair<T1, List<T2>>(default(T1), JsonConvert.DeserializeObject<List<T2>>(output));
            }
            else
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = new KeyValuePair<T1, List<T2>>(default(T1), JsonConvert.DeserializeObject<List<T2>>(output));

                return returnObject;
            }
            return returnObject;
        }

        public async Task<T> DeleteAsync<T>(String serviceUrl)
        {
            T returnObject = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            HttpResponseMessage message = await client.DeleteAsync(serviceUrl);
            if (message.IsSuccessStatusCode)
            {
                string output = await message.Content.ReadAsStringAsync();
                returnObject = JsonConvert.DeserializeObject<T>(output);
            }
            else
            {
            }
            return returnObject;
        }
    }

}