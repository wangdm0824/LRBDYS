using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    public interface IGetAccessToken
    {
        /// <summary>
        /// 生成产生AccessToken的地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        string GenerateAccessTokenUrl(string redirectUrl, string code);
    }
}
