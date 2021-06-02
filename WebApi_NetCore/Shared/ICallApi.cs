using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_NetCore.Model;

namespace WebApi_NetCore.Shared
{
    public interface ICallApi
    {
        JObject CallApi1(string url, string data, string accessToken);
        JObject CallApi2(string url, string data);
        Task<JObject> CallApiGet(string url, string accessToken);
        List<ChiTietGiaoDich> B1(string user, string pass);
        List<string> CatChuoi(string str);
        ChiTietGiaoDich DocFile(string str);
        string TestDocHinh(string base64);
    }
}
