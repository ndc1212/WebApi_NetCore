using Castle.Core.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApi_NetCore.DB;
using WebApi_NetCore.Model;

namespace WebApi_NetCore.Shared
{
    public class CallApi
    {
        //public ILogger Logger { protected get; set; }
        //private readonly DemoDbContext _demoDbContext;
        public CallApi(/*DemoDbContext demoDbContext*/)
        {
            //_demoDbContext = demoDbContext;
        }
        public static int icong = 0;
        public static string Cookies_sessionId = "";
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
                            //acb.dse_applicationId = -1;
                            //acb.dse_pageId = 1;
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
                    //acb.dse_applicationId = -1;
                    await DocHinh(acb.dse_sessionId);
                    //acb.dse_pageId = 2;
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
        public async Task<string> DocHinh1()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Captcha.jpg");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            WebResponse webResponse = webRequest.GetResponse();
            var Cookie = webResponse.Headers["Set-Cookie"];
            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            using (StreamReader responseReader = new StreamReader(webStream))
            {

                var response = responseReader.ReadToEnd();
                using (var memoryStream = new MemoryStream())
                {
                    responseReader.BaseStream.CopyTo(memoryStream);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
             
                //using (var re = responseReader.BaseStream)
                //{
                //    var bytes = re.
                //}
                //return JObject.Parse(response);
            }
            return Cookie;
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
            //data.dse_pageId = -7;
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
        public string getcaptcha(string id)
        {
            string url = "https://online.acb.com.vn/acbib/Captcha.jpg";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                var Cookie = webResponse.Headers["Set-Cookie"];
                //r = GetCookie(Cookie, r);
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        rd.BaseStream.CopyTo(memoryStream);
                        var bytes = memoryStream.ToArray();
                       
                        //Debug.Print(Convert.ToBase64String(bytes));
                        var captcha = TestDocHinh(Convert.ToBase64String(bytes));

                        File.WriteAllBytes("D:\\Captcha\\" + captcha + ".jpg", bytes);
                        return captcha;
                        //return Convert.ToBase64String(bytes);
                        //r.Base64Captcha = ChangeJpg(bytes);
                    }
                }
            }
            //return r;
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


        public List<ThongTinGiaoDich> B1(string user,string pass,DateTime? ngaybatdau)
        {
            if(icong == 1)
            {
                icong = 0;
                Cookies_sessionId = "";
            }
            if (!string.IsNullOrWhiteSpace(Cookies_sessionId))
            {
                return B4(Cookies_sessionId, ngaybatdau);
            }
            else
            {


                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request");
                webRequest.Method = "GET";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                webRequest.Host = "online.acb.com.vn";
                webRequest.Accept = "*/*";
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                webRequest.CookieContainer = new CookieContainer();

                var sessionid = "";

                WebResponse webResponse = webRequest.GetResponse();
                var Cookie = webResponse.Headers["Set-Cookie"];
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {

                    string response = responseReader.ReadToEnd();
                    sessionid = response.Substring(response.IndexOf("dse_sessionId="), 37).Replace("dse_sessionId=", "");
                    //return JObject.Parse(response);
                }

                return B3(sessionid, user, pass, ngaybatdau);
            }
        }
        public string GetTS01(string ck)
        {
            if (ck.IndexOf("TS0124ae4f=") != -1)
            {
                var str = ck.Substring(ck.IndexOf("TS0124ae4f="), ck.IndexOf(";") - ck.IndexOf("TS0124ae4f=")).Replace("TS0124ae4f=", "");
                return str;
            }
            return "";
        }
        public string GetTSa8(string ck)
        {
            if (ck.IndexOf("TS0124ae4f=") != -1)
            {
                string ts2 = ck.Substring(ck.IndexOf("TS0124ae4f="), ck.IndexOf(";") - ck.IndexOf("TS0124ae4f=")) + ";";
                ts2 = ck.Replace(ts2, "");
                ts2 = ts2.Substring(ts2.IndexOf("TSa8cee23f027="), ts2.LastIndexOf(";") - ts2.IndexOf("TSa8cee23f027=")).Replace("TSa8cee23f027=", "");
                return ts2;
            }
            else
            {
                ck = ck.Substring(ck.IndexOf("TSa8cee23f027="), ck.IndexOf(";") - ck.IndexOf("TSa8cee23f027=")).Replace("TSa8cee23f027=", "");
                return ck;
            }            
        }
        //public List<ThongTinGiaoDich> B2(string id,string user,string pass)
        //{
        //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request?&dse_sessionId=" + id + "&dse_applicationId=-1&dse_pageId=2&dse_operationName=displayPageNotLoginOp&dse_errorPage=index.jsp&dse_processorState=initial&pageName=ibk/login.jsp");
        //    webRequest.Method = "GET";
        //    webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
        //    webRequest.Host = "online.acb.com.vn";
        //    webRequest.Accept = "*/*";
        //    webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
        //    webRequest.CookieContainer = new CookieContainer();
           
        //    webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));
        
        //    WebResponse webResponse = webRequest.GetResponse();
        //    var Cookie = webResponse.Headers["Set-Cookie"];
        //    using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
        //    using (StreamReader responseReader = new StreamReader(webStream))
        //    {

        //        string response = responseReader.ReadToEnd();
              
        //    }
           
        //    return B3(id,user,pass);
        //}
        public List<ThongTinGiaoDich> B3(string id,string user,string pass, DateTime? ngaybatdau)
        {
            //id = "olkawdVVo3nNHW0Sbjcf5ea";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request");
            webRequest.Method = "POST";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            
            webRequest.CookieContainer = new CookieContainer();
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id +":-1", "/", "online.acb.com.vn"));
           
            var postData = "dse_sessionId=" + id;
            postData += "&dse_applicationId=" + "-1";
            postData += "&dse_pageId=" + "3";
            postData += "&dse_operationName=" + "obkLoginOp";
            postData += "&dse_errorPage=" + "ibk/login.jsp";
            postData += "&dse_processorState=" + "initial";
            postData += "&UserName=" + user;
            postData += "&PassWord=" + pass;
            postData += "&glbLogedIn=" + "WEB";
            postData += "&SecurityCode=" + getcaptcha(id);
            var data = Encoding.ASCII.GetBytes(postData);

            webRequest.ContentLength = data.Length;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
           
            WebResponse webResponse = webRequest.GetResponse();           
            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            using (StreamReader responseReader = new StreamReader(webStream))
            {

                string response = responseReader.ReadToEnd();
                if(response.IndexOf("obkLoginOp") != -1)
                {
                    Thread.Sleep(2000);
                    B3(id, user, pass,ngaybatdau);
                }
                //return JObject.Parse(response);
            }
            //return B4(id,ngaybatdau);
            return B5(id,ngaybatdau);
        }

        public List<ThongTinGiaoDich> B5(string id,DateTime? ngaybatdau)
        {
            icong++;
            Cookies_sessionId = id;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request");
                webRequest.Method = "POST";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                webRequest.Host = "online.acb.com.vn";
                webRequest.Accept = "*/*";

                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");

                webRequest.CookieContainer = new CookieContainer();
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));


                var postData = "dse_sessionId=" + id;
                postData += "&dse_applicationId=" + "-1";
                postData += "&dse_operationName=" + "ibkacctDetailProc";
                postData += "&dse_pageId=" + "3";
                postData += "&dse_processorState=" + "acctDetailPage";
                postData += "&dse_errorPage=" + "/ibk/acctinquiry/trans.jsp";
                postData += "&AccountNbr=" + "259723809";
                postData += "&CheckRef=" + "false";
                postData += "&EdtRef=" + "";
                postData += "&dse_nextEventName=" + "byDate";
                postData += "&FromDate=" + DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
                postData += "&ToDate=" + DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");

                List<string> lst = new List<string>();

                var data = Encoding.ASCII.GetBytes(postData);

                webRequest.ContentLength = data.Length;

                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                WebResponse webResponse = webRequest.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {

                    string response = responseReader.ReadToEnd();
                    Debug.Print(response);
                    return GetList(response);
                    //if (response.IndexOf("obkLoginOp") != -1)
                    //{
                    //    Thread.Sleep(2000);
                    //    B3(id, user, pass, ngaybatdau);
                    //}
                    //return JObject.Parse(response);
                }
            }catch(Exception ex)
            {
                Thread.Sleep(2000);
                return B5(id,ngaybatdau);
            }
            //return new List<ThongTinGiaoDich>();
        }
        public List<ThongTinGiaoDich> GetList(string html)
        {
            //html = File.ReadAllText("C:\\Users\\admin\\Desktop\\new9.html");
            var res = new List<ThongTinGiaoDich>();
            var lsttd = html.Split("id=\"table1\"").ToList();
            lsttd.RemoveAt(0);
            var stringsau = lsttd[0];
            lsttd = stringsau.Split("</table>").ToList();
            stringsau = lsttd[0];
            lsttd = stringsau.Split("</tr>").ToList();
            lsttd.RemoveAt(lsttd.Count - 1);
            lsttd.RemoveAt(0);

            foreach(var item in lsttd)
            {
                var test = item.Split("<td");
                var gd = new ThongTinGiaoDich();
                gd.TaiKhoan = item.Split("<td")[2].Split(">")[1].Replace("</td", "");
                gd.SoTien = item.Split("<td")[6].Split(">")[1].Replace("</td", "");
                gd.Ngay = item.Split("<td")[1].Split(">")[1].Replace("</td", "");
                gd.TenDangNhap = item.Split("<td")[4].Split("class=\"td_desc\"")[1].Split(">")[1].Replace("</div", "");
                res.Add(gd);
            }


            return res;
        }
        public List<ThongTinGiaoDich> B4(string id, DateTime? ngaybatdau)
        {
            icong++;
            Cookies_sessionId = id;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/Request?&dse_sessionId=" + id + "&dse_applicationId=-1&dse_pageId=4&dse_operationName=ibktransOnlineSumProc&dse_errorPage=/ibk/login.jsp&dse_processorState=initial&dse_nextEventName=start");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            webRequest.Host = "online.acb.com.vn";
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            webRequest.CookieContainer = new CookieContainer();

            List<string> lst = new List<string>();

            webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));
            WebResponse webResponse = webRequest.GetResponse();
            var Cookie = webResponse.Headers["Set-Cookie"];
            string html = "";
            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            using (StreamReader responseReader = new StreamReader(webStream))
            {

                string response = responseReader.ReadToEnd();
                Debug.Print(response);
                html = response;
                //lst = CatChuoi(response);
                //return JObject.Parse(response);
            }
            //var lstchitiet = new List<ChiTietGiaoDich>();
            //foreach(var item in lst)
            //{
            //    lstchitiet.Add(GetList1(item, id));
            //}
            //return GetThongTin(res);
            return GetThongTin(html,id,ngaybatdau);
        }
        public List<ThongTinGiaoDich> GetThongTin(string text,string id, DateTime? ngaybatdau)
        {
            //Debug.Print(icong.ToString());
            //icong++;
             //text = File.ReadAllText("C:\\Users\\admin\\Desktop\\new9.html");
            var lst = new List<ThongTinGiaoDich>();
            var lstnho = text.Split("<tr class=\"table-style-double\">").ToList();
            //lstnho.RemoveAt(lstnho.Count() - 1);
            if (lstnho.Count() > 0)
            {
                lstnho.RemoveAt(0);
            }
            if (lstnho.Count() > 0)
            {
                lstnho.RemoveAt(0);
            }
          
            for(var i = 0; i <= lstnho.Count() - 1; i++)
            {
                var tachtd = lstnho[i].Split("<td").ToList();
                tachtd.RemoveAt(tachtd.Count() - 1);
                tachtd.RemoveAt(0);
                //var test = lstnho[i+1].Split("<td")[3].Split(">")[2].Replace("</div","");
                if (i + 1 < lstnho.Count())
                {
                    if (i % 2 == 0 || i == 0)
                    {
                        var ngay = tachtd[1].Replace("<br/>", " ").Split(">")[1].Replace("</td", "");
                        //var day = ngay.Split("-")[0];
                        //var month = ngay.Split("-")[1];
                        //var ngaymoi = ngay.Replace(day+"-"+month, month+"-"+day);
                        if (ngaybatdau != null)
                        {
                            if (Convert.ToDateTime(ngay.Replace(ngay.Split("-")[0]+"-"+ngay.Split("-")[1], ngay.Split("-")[1] + "-" + ngay.Split("-")[0])) < ngaybatdau.Value.AddSeconds(1))
                            {
                                return lst;
                            }
                        }
                        lst.Add(new ThongTinGiaoDich()
                        {
                            Ngay = ngay,
                            SoTien = tachtd[4].Split(">")[1].Replace("</td", ""),
                            TaiKhoan = tachtd[2].Split(">")[1].Replace("</td", ""),
                            TenDangNhap = lstnho[i + 1].Split("<td")[3].Split(">")[2].Replace("</div", "")
                        });
                    }
                }
            }
            if(lst.Count() == 0)
            {
                Debug.Print("0000000000000");
                Thread.Sleep(2000);
                B4(id,ngaybatdau);
            }
            //while(text.Contains("<tr class=\"table-style-double\">"))
            //{
            //    var dl = "";
            //    while (true)
            //    {
            //        dl += text.IndexOf("<tr class=\"table-style-double\">")
            //    }    
            //}

            return lst;
        }
        public List<string> CatChuoi(string str) {
            var res = new List<string>();
            //str = File.ReadAllText("C:\\Users\\admin\\Desktop\\new9.html");
            while (str.IndexOf("class=\"acc_bold\">Xem") != -1)
            {
                var test = str.IndexOf("class=\"acc_bold\">Xem");
                var test1 = str.Substring(test - 239, 237);
                res.Add(test1);
                str = str.Remove(test,20);
            }            
            return res;
        }
        public ChiTietGiaoDich GetList1(string item, string id)
        {
            Thread.Sleep(500);
            var res = new ChiTietGiaoDich();
            //foreach (var item in str)
            //{
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/" + item);
                webRequest.Method = "GET";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                webRequest.Host = "online.acb.com.vn";
                webRequest.Accept = "*/*";
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                webRequest.CookieContainer = new CookieContainer();

                ChiTietGiaoDich obj = new ChiTietGiaoDich();

                webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));
                WebResponse webResponse = webRequest.GetResponse();
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {

                    string response = responseReader.ReadToEnd();
                    StringWriter myWriter = new StringWriter();
                    HttpUtility.HtmlDecode(response, myWriter);
                    res = DocFile(myWriter.ToString());
                    
                    //lst = CatChuoi(response);
                    //return JObject.Parse(response);
                }
            //}
            return res;
        }
        public List<ChiTietGiaoDich> GetList(List<string> str,string id) {
            var res = new List<ChiTietGiaoDich>();
            var i = 0;
            foreach (var item in str)
            {
                
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://online.acb.com.vn/acbib/" + item );
                webRequest.Method = "GET";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                webRequest.Host = "online.acb.com.vn";
                webRequest.Accept = "*/*";
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                webRequest.CookieContainer = new CookieContainer();

                ChiTietGiaoDich obj = new ChiTietGiaoDich();

                webRequest.CookieContainer.Add(new Cookie("JSESSIONID", "0000" + id + ":-1", "/", "online.acb.com.vn"));
                WebResponse webResponse = webRequest.GetResponse();
                var Cookie = webResponse.Headers["Set-Cookie"];
                Thread.Sleep(2000);
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {

                    string response = responseReader.ReadToEnd();
                    StringWriter myWriter = new StringWriter();
                    HttpUtility.HtmlDecode(response, myWriter);
                    res.Add(DocFile(myWriter.ToString()));
                    File.WriteAllText("D:\\Html\\" + i + ".txt", myWriter.ToString());
                    i++;
                    //lst = CatChuoi(response);
                    //return JObject.Parse(response);
                }
                
            }
            return res;
        }
        public ChiTietGiaoDich DocFile(string str)
        {
            var obj = new ChiTietGiaoDich();
            //var str = File.ReadAllText("C:\\Users\\admin\\Downloads\\ACBOnline.html");
            //str = str.Replace(" ", "");
            //1
            if (str.IndexOf("<td class=\"caption\">Số</td>") != -1)
            {
                string so = "";
                var i = 27;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td class=\"caption\">Số</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.So_P1 = so.Replace("\n","").Replace("\r", "").Replace("<td>","")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>","").Trim();
            }
            
            if (str.IndexOf("<td>Ngày lập </td>") != -1)
            {
                string so = "";
                var i = 18;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Ngày lập </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NgayLap_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td >Ngày lập </td>") != -1)
            {
                string so = "";
                var i = 19;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td >Ngày lập </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NgayLap_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Trạng thái </td>") != -1)
            {
                string so = "";
                var i = 20;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Trạng thái </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TrangThai_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td >Trạng thái </td>") != -1)
            {
                string so = "";
                var i = 21;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td >Trạng thái </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TrangThai_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Tên đơn vị trả tiền </td>") != -1)
            {
                string so = "";
                var i = 29;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Tên đơn vị trả tiền </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TenDonViDaTraTien_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
          
            if (str.IndexOf("<td>Tài khoản số </td>") != -1)
            {
                string so = "";
                var i = 22;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Tài khoản số </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiKhoanSo_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td >Tài khoản số </td>") != -1)
            {
                string so = "";
                var i = 23;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td >Tài khoản số </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiKhoanSo_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Tại ngân hàng</td>") != -1)
            {
                string so = "";
                var i = 22;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Tại ngân hàng</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiNganHang_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Tại ngân hàng</td>") != -1)
            {
                string so = "";
                var i = 34;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Tại ngân hàng</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiNganHang_P1 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            //2
            if (str.IndexOf("<td>Tên đơn vị nhận tiền </td>") != -1)
            {
                string so = "";
                var i = 30;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Tên đơn vị nhận tiền </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TenDonViNhanTien_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Tên đơn vị nhận tiền </td>") != -1)
            {
                string so = "";
                var i = 42;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Tên đơn vị nhận tiền </td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TenDonViNhanTien_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Số CMND /Passport</td>") != -1)
            {
                string so = "";
                var i = 26;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Số CMND /Passport</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.SoCMND_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Số CMND /Passport</td>") != -1)
            {
                string so = "";
                var i = 38;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Số CMND /Passport</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.SoCMND_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Ngày cấp</td>") != -1)
            {
                string so = "";
                var i = 17;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Ngày cấp</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NgayCap_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Ngày cấp</td>") != -1)
            {
                string so = "";
                var i = 29;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Ngày cấp</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NgayCap_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Nơi cấp</td>") != -1)
            {
                string so = "";
                var i = 16;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Nơi cấp</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NoiCap_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Nơi cấp</td>") != -1)
            {
                string so = "";
                var i = 28;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Nơi cấp</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NoiCap_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.LastIndexOf("<td>Tài khoản số</td>") != -1)
            {
                string so = "";
                var i = 21;
                while (true)
                {
                    so += str.Substring(str.LastIndexOf("<td>Tài khoản số</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiKhoanSo_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.LastIndexOf("<td height=\"25\">Tài khoản số</td>") != -1)
            {
                string so = "";
                var i = 33;
                while (true)
                {
                    so += str.Substring(str.LastIndexOf("<td height=\"25\">Tài khoản số</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiKhoanSo_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.LastIndexOf("<td>Tại ngân hàng</td>") != -1)
            {
                string so = "";
                var i = 22;
                while (true)
                {
                    so += str.Substring(str.LastIndexOf("<td>Tại ngân hàng</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiNganHang_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.LastIndexOf("<td height=\"25\">Tại ngân hàng</td>") != -1)
            {
                string so = "";
                var i = 34;
                while (true)
                {
                    so += str.Substring(str.LastIndexOf("<td height=\"25\">Tại ngân hàng</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.TaiNganHang_P2 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            //3
            if (str.IndexOf("<td>Số tiền</td>") != -1)
            {
                string so = "";
                var i = 16;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Số tiền</td>") + i, 1);
                    i++;
                    if (so.Contains("VND") || so.Contains("USD"))
                    {
                        break;
                    }
                }
                obj.SoTien_P3 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td height=\"25\">Số tiền</td>") != -1)
            {
                string so = "";
                var i = 28;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td height=\"25\">Số tiền</td>") + i, 1);
                    i++;
                    if (so.Contains("VND") || so.Contains("USD"))
                    {
                        break;
                    }
                }
                obj.SoTien_P3 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }

            if (str.IndexOf("<td>Số tiền bằng chữ</td>") != -1)
            {
                string so = "";
                var i = 25;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Số tiền bằng chữ</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.SoTienBangChu_P3 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Replace("<script>document.write(chu());</script>","").Trim();
            }
            else if (str.IndexOf("<td  height=\"25\">Số tiền bằng chữ</td>") != -1)
            {
                string so = "";
                var i = 38;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td  height=\"25\">Số tiền bằng chữ</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.SoTienBangChu_P3 = so.Replace("\n", "").Replace("\r", "")
                    .Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Replace("<script>document.write(chu());</script>", "").Trim();
            }

            if (str.IndexOf("<td>Nội dung chuyển khoản</td>") != -1)
            {
                string so = "";
                var i = 30;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td>Nội dung chuyển khoản</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NoiDungChuyenKhoan_P3 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            else if (str.IndexOf("<td  height=\"25\">Nội dung chuyển khoản</td>") != -1)
            {
                string so = "";
                var i = 43;
                while (true)
                {
                    so += str.Substring(str.IndexOf("<td  height=\"25\">Nội dung chuyển khoản</td>") + i, 1);
                    i++;
                    if (so.Contains("</td>"))
                    {
                        break;
                    }
                }
                obj.NoiDungChuyenKhoan_P3 = so.Replace("\n", "").Replace("\r", "").Replace("<td>", "")
                    .Replace("<td >", "")
                    .Replace("<td  >", "")
                    .Replace("</td>", "").Trim();
            }
            return obj;
        }
        public string TestDocHinh(string base64)
        {
            var bit = Base64StringToBitmap(base64);
            //var method = new MethodsCaptchaSolver();
            bit = new Bitmap(bit);
            return MethodsCaptchaSolver.OCR(bit);
        }
        public static Bitmap Base64StringToBitmap(string base64String)
        {
            
            Bitmap bmpReturn = null;
            //Convert Base64 string to byte[]
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }
    }
}
