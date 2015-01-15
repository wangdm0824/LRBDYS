using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Common;
using LRBDYS.OAuthPlugin.Weibo.Config;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.AccessToken.Provider.Sina
{
    public class SinaAuthorizationCode : IAuthorizationCodeBase
    {
        private static AppConfig _appConfig = AppConfigs.GetSina();

        #region IGetCode 成员

        /// <summary>
        /// 生成产生Code的地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public string GenerateCodeUrl(string redirectUrl)
        {
            return string.Format("https://api.weibo.com/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}", _appConfig.AppKey, redirectUrl);
        }

        #endregion

        #region IGetAccessToken 成员

        /// <summary>
        /// 生成产生AccessToken的地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GenerateAccessTokenUrl(string redirectUrl, string code)
        {
            return string.Format("https://api.weibo.com/oauth2/access_token?client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={2}&code={3}", _appConfig.AppKey, _appConfig.AppSecret, redirectUrl, code);
        }

        #endregion

        #region IAccessToken 成员

        /// <summary>
        /// 通过AccessToken获得结果
        /// </summary>
        /// <param name="getAccessTokenUrl"></param>
        /// <returns></returns>
        public dynamic GetResult(string getAccessTokenUrl)
        {
            ClientRequest cr = new ClientRequest(getAccessTokenUrl);
            cr.HttpMethod = WebRequestMethods.Http.Post;

            return NetQuick.GetResponseForDynamic(cr);
        }

        #endregion

    }
}
