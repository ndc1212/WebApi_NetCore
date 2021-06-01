using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Shared
{
    public interface ICallApi
    {
        JObject CallApi1(string url, string data, string accessToken);
        JObject CallApi2(string url, string data);
        Task<JObject> CallApiGet(string url, string accessToken);
    }
}
