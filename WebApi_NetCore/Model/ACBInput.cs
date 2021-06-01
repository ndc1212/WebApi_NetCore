using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Model
{
    public class ACBInput
    {
        public string dse_sessionId { get; set; }
        public int dse_applicationId { get; set; }
        public int dse_pageId { get; set; }
        public string dse_operationName { get; set; }
        public string dse_errorPage { get; set; }
        public string dse_processorState { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string glbLogedIn { get; set; }
        public string SecurityCode { get; set; }
    }
}
