using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Common;
using LRBDYS.OAuthPlugin.Weibo.Config;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.AccessToken.Provider.Tencent
{
    public class TencentAuthorizationCode : IAuthorizationCodeBase
    {
        /// <summary>
        /// 服务商AppKey,AppSecret
        /// </summary>
        private static AppConfig _appConfig = AppConfigs.GetTencent();

        #region IGetCode 成员

        /// <summary>
        /// 生成产生Code的地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public string GenerateCodeUrl(string redirectUrl)
        {
            return string.Format("https://open.t.qq.com/cgi-bin/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}", _appConfig.AppKey, redirectUrl);
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
            return string.Format("https://open.t.qq.com/cgi-bin/oauth2/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&grant_type=authorization_code&code={3}", _appConfig.AppKey, _appConfig.AppSecret, redirectUrl, code);
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
            cr.HttpMethod = WebRequestMethods.Http.Get;      //腾讯微创新，走GET路线~~新浪POST靠边站

            string src = NetQuick.GetResponseForText(cr);     //返回值也微创新了！ {key}={value}&{key..n}={value..n}

            var arr = src.Split('&');
            var list = new Dictionary<string, string>();
            foreach (var item in arr)
            {
                var tmp = item.Split('=');
                list.Add(tmp[0], tmp[1]);
            }

            var access_token = list["access_token"];
            var expires_in = list["expires_in"];
            var refresh_token = list["refresh_token"];
            var name = list["name"];
            var nick = list["nick"];

            dynamic result = new DynamicDictionary();
            result.access_token = access_token;
            result.expires_in = expires_in;
            result.refresh_token = refresh_token;
            result.name = name;
            result.nick = nick;

            return result;
        }

        #endregion

    }
}
