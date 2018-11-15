using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// HttpClient
    /// </summary>
    public class HttpClientHelper
    {
        private static readonly string userAgen = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";

        /// <summary>
        /// 根据Url地址Get请求返回数据
        /// 2015年11月12日14:50:02
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="ch_httpcode">http状态码</param>
        /// <returns>字符串</returns>
        public static string GetResponse(string url, out string httpStatusCode)
        {
            httpStatusCode = string.Empty;
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                //httpClient = new HttpClient();
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> taskResponse = httpClient.GetAsync(url);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{

                //HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    //此处会抛出异常：不支持超时设置，对返回结果没有影响
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();

                    return result;
                }
                //}
                return null;
                //}
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据Url地址Get请求返回数据
        /// xuja
        /// 2015年11月12日14:50:02
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>字符串</returns>
        public static string GetResponse(string url)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                //httpClient = new HttpClient();
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> taskResponse = httpClient.GetAsync(url);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{

                //HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();

                    return result;
                }
                //}
                return null;
                //}
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }
            }
        }


        /// <summary>
        /// 根据Url地址Get请求返回实体
        /// xuja
        /// 2015年11月12日14:50:02
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>实体</returns>
        public static T GetResponse<T>(string url)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                //httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = httpClient.GetAsync(url).Result;
                Task<HttpResponseMessage> taskResponse = httpClient.GetAsync(url);
                taskResponse.Wait();
                T result = default(T);
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string s = reader.ReadToEnd();

                    result = JsonConvert.DeserializeObject<T>(s);

                }
                //}
                return result;
                //}
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.ToString()),
                    ReasonPhrase = "error"
                };
                //throw new HttpResponseException(resp); 
                return default(T);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }
        }


        /// <summary>
        /// 处理Get的Url
        /// des：huyf
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string WithParameters(Dictionary<string, string> dic)
        {
            string result = "?";
            foreach (var item in dic)
            {
                result += item.Key + "=" + item.Value + "&";
            }
            result = result.Remove(result.Length - 1);

            return result;
        }

        /// <summary>
        /// Post请求返回字符
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>字符</returns>
        public static string PostResponse(string url, string postData)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                //HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();
                    return result;
                }
                //}
                return null;
                //}
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }
        /// <summary>
        /// Post请求返回字符
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>字符</returns>
        public static string PostResponse(string url, object obj)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                string postData = JsonConvert.SerializeObject(obj);
                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                //HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();
                    return result;
                }
                else
                {
                    //LogHelper.WriteInfo(typeof(HttpClientHelper), "结果：" + response.StatusCode + "内容：" + JsonConvertTool.SerializeObject(obj));
                }
                //}
                return null;
                //}
            }
            catch (Exception exception)
            {
                //LogHelper.Error("结果：异常 内容：" + JsonConvertTool.SerializeObject(obj) + "ex:" + exception.Message, exception);
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }
        /// <summary>
        /// Post请求返回实体 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>实体</returns>
        public static T PostResponse<T>(string url, string postData)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                T result = default(T);
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                //HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string s = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(s);
                }
                //}
                return result;
                //}
            }
            catch
            {
                return default(T);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }

        /// <summary>
        /// Post请求返回实体
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>实体</returns>
        public static T PostResponse<T>(string url, object obj)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                string postData = JsonConvert.SerializeObject(obj);
                HttpContent httpContent = new StringContent(postData);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                T result = default(T);
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                //HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string s = reader.ReadToEnd();

                    result = JsonConvert.DeserializeObject<T>(s);
                }
                //}
                return result;
                //}
            }
            catch
            {
                return default(T);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }
            }
        }


        /// <summary>
        /// Put请求返回字符
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>字符</returns>
        public static string PutResponse(string url, object obj)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                string postData = JsonConvert.SerializeObject(obj);
                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                //using (HttpResponseMessage response = taskResponse.Result)
                //{
                //HttpResponseMessage response = httpClient.PutAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();
                    return result;
                }
                //}
                return null;
                //}
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }
            }
        }

        /// <summary>
        /// 将Http状态码翻译为对应的中文【暂未使用】
        /// </summary>
        /// <param name="code">Http状态码</param>
        /// <returns>中文解析</returns>
        public static string ToChsText(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Continue:
                    return "请求者应继续进行请求";
                case HttpStatusCode.SwitchingProtocols:
                    return "请求者已要求服务器切换协议，服务器已确认并准备进行切换";
                case HttpStatusCode.OK:
                    return "服务器成功处理了相应请求";
                case HttpStatusCode.Created:
                    return "请求成功且服务器已创建了新的资源";
                case HttpStatusCode.Accepted:
                    return "服务器已接受相应请求，但尚未对其进行处理";
                case HttpStatusCode.NonAuthoritativeInformation:
                    return "服务器已成功处理相应请求，但返回了可能来自另一来源的信息";
                case HttpStatusCode.NoContent:
                    return "服务器已成功处理相应请求，但未返回任何内容";
                case HttpStatusCode.ResetContent:
                    return "服务器已成功处理相应请求，但未返回任何内容，但要求请求者重置文档视图";
                case HttpStatusCode.PartialContent:
                    return "服务器成功处理了部分 GET 请求";
                case HttpStatusCode.MultipleChoices:
                    return "服务器可以根据请求来执行多项操作";
                case HttpStatusCode.Moved:
                    return "请求的网页已永久移动到新位置";
                case HttpStatusCode.Redirect:
                    return "服务器目前正从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求";
                case HttpStatusCode.RedirectMethod:
                    return "当请求者应对不同的位置进行单独的 GET 请求以检索响应时，服务器会返回此代码";
                case HttpStatusCode.NotModified:
                    return "请求的网页自上次请求后再也没有修改过";
                case HttpStatusCode.UseProxy:
                    return "请求者只能使用代理访问请求的网页";
                case HttpStatusCode.Unused:
                    return "Unused 是未完全指定的 HTTP/1.1 规范的建议扩展";
                case HttpStatusCode.RedirectKeepVerb:
                    return "服务器目前正从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求";
                case HttpStatusCode.BadRequest:
                    return "服务器未能识别请求";
                case HttpStatusCode.Unauthorized:
                    return "请求要求进行身份验证";
                case HttpStatusCode.PaymentRequired:
                    return "保留 PaymentRequired 以供将来使用";
                case HttpStatusCode.Forbidden:
                    return "服务器拒绝相应请求";
                case HttpStatusCode.NotFound:
                    return "服务器找不到请求的资源";
                case HttpStatusCode.MethodNotAllowed:
                    return "禁用相应请求中所指定的方法";
                case HttpStatusCode.NotAcceptable:
                    return "无法使用相应请求的内容特性来响应请求的网页";
                case HttpStatusCode.ProxyAuthenticationRequired:
                    return "请求者应当使用代理进行授权";
                case HttpStatusCode.RequestTimeout:
                    return "服务器在等待请求时超时";
                case HttpStatusCode.Conflict:
                    return "服务器在完成请求时遇到冲突";
                case HttpStatusCode.Gone:
                    return "请求的资源已被永久删除";
                case HttpStatusCode.LengthRequired:
                    return "服务器不会接受包含无效内容长度标头字段的请求";
                case HttpStatusCode.PreconditionFailed:
                    return "服务器未满足请求者在请求中设置的其中一个前提条件";
                case HttpStatusCode.RequestEntityTooLarge:
                    return "服务器无法处理相应请求，因为请求实体过大，已超出服务器的处理能力";
                case HttpStatusCode.RequestUriTooLong:
                    return "请求的 URI 过长，服务器无法进行处理";
                case HttpStatusCode.UnsupportedMediaType:
                    return "相应请求的格式不受请求页面的支持";
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    return "如果相应请求是针对网页的无效范围进行的，那么服务器会返回此状态代码";
                case HttpStatusCode.ExpectationFailed:
                    return "服务器未满足“期望”请求标头字段的要求";
                case HttpStatusCode.InternalServerError:
                    return "服务器内部遇到错误，无法完成相应请求";
                case HttpStatusCode.NotImplemented:
                    return "请求的功能在服务器中尚未实现";
                case HttpStatusCode.BadGateway:
                    return "服务器作为网关或代理，从上游服务器收到了无效的响应";
                case HttpStatusCode.ServiceUnavailable:
                    return "目前服务器不可用（由于超载或进行停机维护）";
                case HttpStatusCode.GatewayTimeout:
                    return "服务器作为网关或代理，未及时从上游服务器接收请求";
                case HttpStatusCode.HttpVersionNotSupported:
                    return "服务器不支持相应请求中所用的 HTTP 协议版本";
                default:
                    return "未知Http状态";
            }
        }

    }

}
