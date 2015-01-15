using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    public interface IAccessToken
    {
        /// <summary>
        /// 通过AccessToken获得结果
        /// </summary>
        /// <param name="getAccessTokenUrl"></param>
        /// <returns></returns>
        dynamic GetResult(string getAccessTokenUrl);
    }
}
