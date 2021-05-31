using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Model
{
    public static class WebConsts
    {

        public const string SwaggerUiEndPoint = "/swagger";
        public const string HangfireDashboardEndPoint = "/hangfire";

        public static bool SwaggerUiEnabled = true;
        public static bool HangfireDashboardEnabled = true;

        public static List<string> ReCaptchaIgnoreWhiteList = new List<string>
        {
            CyberTechConsts.AbpApiClientUserAgent
        };

        public static class GraphQL
        {
            public const string PlaygroundEndPoint = "/ui/playground";
            public const string EndPoint = "/graphql";

            public static bool PlaygroundEnabled = true;
            public static bool Enabled = true;
        }
    }
    public class CyberTechConsts
    {
        public const string LocalizationSourceName = "CyberTech";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const bool AllowTenantsToChangeEmailSettings = false;

        public const string Currency = "USD";

        public const string CurrencySign = "đ";

        public const string AbpApiClientUserAgent = "AbpApiClient";
    }
}
