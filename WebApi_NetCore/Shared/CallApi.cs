using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi_NetCore.Model;

namespace WebApi_NetCore.Shared
{
    public class CallApi : ICallApi
    {
        //public ILogger Logger { protected get; set; }

        public JObject CallApi1(string url, string data, string accessToken)
        {
            //Logger.Error($"LOG ẤN NÚT HOÀN THÀNH CALLAPI url: {url}, data: {data}, accessToken: { accessToken}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.KeepAlive = true;

            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            byte[] dt = Encoding.UTF8.GetBytes(data);
            try
            {
                if (dt.Length > 0)
                    using (Stream webStream = request.GetRequestStream())
                    {
                        using (BufferedStream requestWriter = new BufferedStream(webStream, (int)dt.LongLength))
                        {
                            requestWriter.Write(dt, 0, (int)dt.LongLength);
                        }
                    }

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return JObject.Parse(response);
                }
            }
            catch (Exception ex)
            {
                // Logger.Error($"LỖI ẤN NÚT HOÀN THÀNH CALLAPI url: {url}, data: {data}, accessToken: { accessToken}");
                //Logger.Error($"Url: {url}, data: {data}.", ex);
                throw;
            }
        }
        public async Task<JObject> CallApiGet(string user, string password)
        {
            var test = new JObject();

            var acb = new ACBInput();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/webmbtt");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            webRequest.CookieContainer = new CookieContainer();
            //webRequest.Timeout = 5000;
            WebResponse webResponse;
            WebResponse webResponsev2;

            Cookie TS0124ae4f = new Cookie();
            Cookie TSa8cee23f027 = new Cookie();

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            //asyncResult.AsyncWaitHandle.WaitOne();

            using (webResponse = webRequest.EndGetResponse(asyncResult))
            {
                var Cookie = webResponse.Headers["Set-Cookie"];
                Debug.Print("Cookie");
                Debug.Print(Cookie);
                //var Location = webResponse.Headers["Location"];
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    var html1 = rd.ReadToEnd();
                    HttpWebRequest webRequestv2 = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/webmbtt");
                    webRequestv2.Method = "GET";
                    webRequestv2.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                    webRequestv2.Host = "online.acb.com.vn";
                    webRequestv2.Accept = "*/*";
                    webRequestv2.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    webRequestv2.CookieContainer = new CookieContainer();
                    //webRequest.Timeout = 5000;
                    string ts1 = Cookie.Substring(Cookie.IndexOf("TS0124ae4f="), Cookie.IndexOf(";") - Cookie.IndexOf("TS0124ae4f=")).Replace("TS0124ae4f=", "");
                    string ts2 = Cookie.Substring(Cookie.IndexOf("TS0124ae4f="), Cookie.IndexOf(";") - Cookie.IndexOf("TS0124ae4f=")) + ";";
                    ts2 = Cookie.Replace(ts2, "");
                    ts2 = ts2.Substring(ts2.IndexOf("TSa8cee23f027="), ts2.LastIndexOf(";") - ts2.IndexOf("TSa8cee23f027=")).Replace("TSa8cee23f027=", "");
                    TS0124ae4f = new Cookie("TS0124ae4f", ts1, "/", "online.acb.com.vn");
                    TS0124ae4f.Secure = true;
                    TS0124ae4f.HttpOnly = true;
                    TSa8cee23f027 = new Cookie("TSa8cee23f027", ts2 , "/", "online.acb.com.vn");
                    webRequestv2.CookieContainer.Add(TS0124ae4f);
                    webRequestv2.CookieContainer.Add(TSa8cee23f027);

                    IAsyncResult asyncResultv2 = webRequestv2.BeginGetResponse(null, null);
                    //asyncResult.AsyncWaitHandle.WaitOne();
                    using (webResponsev2 = webRequestv2.EndGetResponse(asyncResultv2))
                    {
                        //var Cookie = webResponse.Headers["Set-Cookie"];
                        //var Location = webResponse.Headers["Location"];
                        using (StreamReader rdv2 = new StreamReader(webResponsev2.GetResponseStream()))
                        {
                            var html = rdv2.ReadToEnd();
                            //await DocHinh();
                            //Debug.Print("html");
                            //Debug.Print(html);
                            acb.UserName = "01652992622";
                            acb.PassWord = "Cocaca@1";
                            acb.dse_sessionId = html.Substring(html.IndexOf("dse_sessionId="), 37).Replace("dse_sessionId=","");
                            acb.dse_applicationId = -1;
                            acb.dse_pageId = 1;
                            acb.dse_operationName = "obkLoginOp";
                            acb.dse_errorPage = "ibk/login.jsp";
                            acb.dse_processorState = "initial";
                            acb.glbLogedIn = "WEB";
                            acb.SecurityCode = "0NXCP";
                            
                        }
                    }
                }
            }
            await DangNhap(acb);


            return test;
        }
        public async Task<JObject> CallApiGet1(string user, string password)
        {
            var test = new JObject();

            var acb = new ACBInput();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request?&dse_sessionId=MepvJ4j_LkE7vY62yE76z6_&dse_applicationId=-1&dse_pageId=2&dse_operationName=displayPageNotLoginOp&dse_errorPage=index.jsp&dse_processorState=initial&pageName=ibk/login.jsp");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            webRequest.CookieContainer = new CookieContainer();

            var a6dee3644a04370ef8ea727fbaedef18 = new Cookie("a6dee3644a04370ef8ea727fbaedef18", "3frf9bsv9d02v79qobl9ll42k0", "/", "online.acb.com.vn");
            var QSI_SI_eQB828lbc7JOBX7_intercept = new Cookie("QSI_SI_eQB828lbc7JOBX7_intercept", "true", "/", "online.acb.com.vn");
            var JSESSIONID = new Cookie("JSESSIONID", "0000MepvJ4j_LkE7vY62yE76z6_:-1", "/", "online.acb.com.vn");
            
            webRequest.CookieContainer.Add(a6dee3644a04370ef8ea727fbaedef18);
            webRequest.CookieContainer.Add(QSI_SI_eQB828lbc7JOBX7_intercept);
            webRequest.CookieContainer.Add(JSESSIONID);
            webRequest.CookieContainer.Add(new Cookie("TS0124ae4f", "01da59b7e4623461479b4babb5dd65259b3daf68c841e4c4bf1984fb6f3c33e9fbd7dbfa969a42ce3b36c7e2d55da24627c7c32155760dd3a8766995c4495a3cd9c6bf5939e8893bde762b4c6548af0c947eb607c67b4c4ffe30c852aa0b9d620d05e3b54bd7401f4f857d216e49f0d05730e05527507250ecb5302b996a3fd285bf6b7906a7bd3e21e699df9570dc98fa13c9d5b3", "/", "online.acb.com.vn"));
            webRequest.CookieContainer.Add(new Cookie("TSa8cee23f027", "08e9eb8c8cab200065a0e525c3a62e16acb3110b0bc61574299f46a9a77eb6d76b39918da609748a0879340ffa1130007589a6247d7d6364ae79741f99f14b6281d11f6863497573f66ac1e2a3b9e299cde0f26ffbbe9ca24dd043d018c5c69c", "/", "online.acb.com.vn"));
            
            //webRequest.Timeout = 5000;
            WebResponse webResponse;
           


            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            //asyncResult.AsyncWaitHandle.WaitOne();

            using (webResponse = webRequest.EndGetResponse(asyncResult))
            {
                var Cookie = webResponse.Headers["Set-Cookie"];
                Debug.Print("Cookie");
                Debug.Print(Cookie);
                //var Location = webResponse.Headers["Location"];
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    var html1 = rd.ReadToEnd();
                    
                    //Debug.Print("html");
                    //Debug.Print(html);
                    acb.UserName = "01652992622";
                    acb.PassWord = "Cocaca@1";
                    acb.dse_sessionId = html1.Substring(html1.IndexOf("dse_sessionId="), 37).Replace("dse_sessionId=", "");
                    acb.dse_applicationId = -1;
                    await DocHinh(acb.dse_sessionId);
                    acb.dse_pageId = 2;
                    acb.dse_operationName = "obkLoginOp";
                    acb.dse_errorPage = "ibk/login.jsp";
                    acb.dse_processorState = "initial";
                    acb.glbLogedIn = "WEB";
                    acb.SecurityCode = "0W4W1";
                }
            }
            await DangNhap(acb);
           

            return test;
        }
        public async Task DangNhap(ACBInput data)
        {
          
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request");
            webRequest.Method = "POST";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            webRequest.CookieContainer = new CookieContainer();
            var a6dee3644a04370ef8ea727fbaedef18 = new Cookie("a6dee3644a04370ef8ea727fbaedef18", "3frf9bsv9d02v79qobl9ll42k0", "/", "online.acb.com.vn");
            var QSI_SI_eQB828lbc7JOBX7_intercept = new Cookie("QSI_SI_eQB828lbc7JOBX7_intercept", "true", "/", "online.acb.com.vn");
            var JSESSIONID = new Cookie("JSESSIONID", "0000MepvJ4j_LkE7vY62yE76z6_:-1", "/", "online.acb.com.vn");

            data.dse_sessionId = "MepvJ4j_LkE7vY62yE76z6_";
            data.dse_pageId = -7;
            data.SecurityCode = "7HA9K";
           

            webRequest.CookieContainer.Add(a6dee3644a04370ef8ea727fbaedef18);
            webRequest.CookieContainer.Add(QSI_SI_eQB828lbc7JOBX7_intercept);
            webRequest.CookieContainer.Add(JSESSIONID);
            webRequest.CookieContainer.Add(new Cookie("TS0124ae4f", "01da59b7e4623461479b4babb5dd65259b3daf68c841e4c4bf1984fb6f3c33e9fbd7dbfa969a42ce3b36c7e2d55da24627c7c32155760dd3a8766995c4495a3cd9c6bf5939e8893bde762b4c6548af0c947eb607c67b4c4ffe30c852aa0b9d620d05e3b54bd7401f4f857d216e49f0d05730e05527507250ecb5302b996a3fd285bf6b7906a7bd3e21e699df9570dc98fa13c9d5b3", "/", "online.acb.com.vn"));
            webRequest.CookieContainer.Add(new Cookie("TSa8cee23f027", "08e9eb8c8cab2000e2ca4db86a278c1d6de30fd430401540758d6681f9ec935af64ca4bb2f98564808368c69be113000c599367e83f7603fd8437659676b83e21f33541b5e08a2e25ff382e14670224fb240ed2100854e1ae039da992086c4b3", "/", "online.acb.com.vn"));
            
            byte[] dt = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            try
            {
                using (Stream webStream = webRequest.GetRequestStream())
                {
                    using (BufferedStream requestWriter = new BufferedStream(webStream, (int)dt.LongLength))
                    {
                        requestWriter.Write(dt, 0, (int)dt.LongLength);
                    }
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    var Cookie = webResponse.Headers["Set-Cookie"];
                    string response = responseReader.ReadToEnd();
                    Debug.Print("response");
                    Debug.Print(response);
                    //return JObject.Parse(response);
                }
            }
            catch (Exception ex)
            {
                //Logger.Error($"Url: {url}, data: {data}.", ex);
                throw;
            }
        }
        public async Task DocHinh(string a)
        {
            var credentials = new NetworkCredential();
            using (var handler = new HttpClientHandler { Credentials = credentials })
            using (var client = new HttpClient(handler))
            {
                var bytes = await client.GetByteArrayAsync("https://online.acb.com.vn/acbib/Captcha.jpg");
                Debug.Print(Convert.ToBase64String(bytes));

            }


        }

        public JObject CallApi2(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Captcha.jpg");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.KeepAlive = true;

            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            byte[] dt = Encoding.UTF8.GetBytes(data);
            try
            {
                using (Stream webStream = request.GetRequestStream())
                {
                    using (BufferedStream requestWriter = new BufferedStream(webStream, (int)dt.LongLength))
                    {
                        requestWriter.Write(dt, 0, (int)dt.LongLength);
                    }
                }

                using (WebResponse webResponse = request.GetResponse())
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return JObject.Parse(response);
                }
            }
            catch (Exception ex)
            {
                //Logger.Error($"Url: {url}, data: {data}.", ex);
                throw;
            }

        }
        public AuthenticateResultModel GetToken(string url, string UserWS, string PassWS)
        {
            //var url = HostApiGuiCrm + GetTokenPath;
            AuthenticateModel ip = new AuthenticateModel();
            ip.UserNameOrEmailAddress = UserWS;
            ip.Password = PassWS;
            var data = JsonConvert.SerializeObject(ip);
            var result = CallApi1(url, data, "");
            var o = JsonConvert.DeserializeObject<AuthenticateResultModel>(result["result"].ToString());
            return o;
        }

    }
}
