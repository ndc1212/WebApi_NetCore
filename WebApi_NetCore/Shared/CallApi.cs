using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi_NetCore.Model;

namespace WebApi_NetCore.Shared
{
    public class CallApi
    {
        public ILogger Logger { protected get; set; }
        public JObject CallApi1(string url, string data, string accessToken)
        {
            Logger.Error($"LOG ẤN NÚT HOÀN THÀNH CALLAPI url: {url}, data: {data}, accessToken: { accessToken}");
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
                Logger.Error($"LỖI ẤN NÚT HOÀN THÀNH CALLAPI url: {url}, data: {data}, accessToken: { accessToken}");
                Logger.Error($"Url: {url}, data: {data}.", ex);
                throw;
            }
        }
        public JObject CallApi2(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
                Logger.Error($"Url: {url}, data: {data}.", ex);
                throw;
            }

        }
        public AuthenticateResultModel GetToken(string url, string UserWS,string PassWS)
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
