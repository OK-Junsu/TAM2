using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestView
{
    public static class APIFunctions
    {
        public static string JsonSerialize<T>(T originData)
        {
            return JsonSerializer.Serialize(originData);
        }
        public static T JsonDeserialize<T>(string jsonData, bool propertyNameCaseInsensitive)
        {
            return JsonSerializer.Deserialize<T>(jsonData, options: new()
            {
                PropertyNameCaseInsensitive = propertyNameCaseInsensitive
            });
        }
        public static string CallWebClient(string url)
        {
            var result = string.Empty;

            try
            {
                var client = new WebClient();

                //특정 요청 헤더값을 추가해준다. 
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using (var data = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(data))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                        data.Close();
                    }
                }

            }
            catch (Exception e)
            {
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }

            return result;
        }

        public static string CallWebRequest_Get(string url)
        {
            var responseFromServer = string.Empty;

            try
            {
                var request = WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers["user-agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                using (var response = request.GetResponse())
                using (var dataStream = response.GetResponseStream())
                using (var reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return responseFromServer;
        }
        public static string Get(string url)
        {
            try
            {
                var client = new WebClient();
                SetWebClientHeader(client);
                return client.DownloadString(url);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Post(string url, string data)
        {
            try
            {
                var client = new WebClient();
                SetWebClientHeader(client);
                client.Dispose();
                client.UploadString(url, "POST", data);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Put(string url, string data)
        {
            try
            {
                var client = new WebClient();
                SetWebClientHeader(client);
                client.UploadString(url, "PUT", data);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Delete(string url)
        {
            try
            {
                var client = new WebClient();
                client.UploadString(url, "DELETE", string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static void SetWebClientHeader(WebClient client)
        {
            try
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                //client.Headers[HttpRequestHeader.AcceptLanguage] = "ko-KR";
                //client.Headers.Add(HttpRequestHeader.Authorization, token);
                client.Encoding = Encoding.UTF8;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
