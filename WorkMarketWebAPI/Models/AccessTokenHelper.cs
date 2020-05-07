using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WorkMarketWebAPI.Models
{
    public class AccessTokenHelper
    {
        private static string Token { get; set; }

        private static string Secret { get; set; }

        private static string accessToken { get; set; }

        //public string AccessToken
        //{
        //    get
        //    {
        //        if (accessToken == null)
        //        {
        //            accessToken = GetToken().Result;
        //        }

        //        return accessToken;
        //    }
        //    set
        //    {
        //        accessToken = value;
        //    }
        //}

        static AccessTokenHelper()
        {
            Token = Convert.ToString(ConfigurationManager.AppSettings["WorkMarketToken"]);
            Secret = Convert.ToString(ConfigurationManager.AppSettings["WorkMarketSecret"]);
        }

        public async Task ResetAccessToken()
        {
            accessToken = await GetToken();
        }

        public async Task<string> GetToken()
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                WorkMarketApiInterface workMarketApiInterface = WorkMarketApiInterface.GetInstance();
                ResponseCommon responseCommon = await workMarketApiInterface.RequestAccessToken(Token, Secret);

                if (responseCommon != null && responseCommon.Response != null)
                {
                    accessToken = responseCommon.Response.AccessToken;
                }
            }

            return accessToken;
        }
    }
}