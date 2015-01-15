using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.AccessToken.Provider.Sina;
using LRBDYS.OAuthPlugin.Weibo.AccessToken.Provider.Tencent;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.AccessToken
{
    /// <summary>
    /// 接口构造机制
    /// </summary>
    public class CreateAT
    {
        public static IAuthorizationCodeBase Sina()
        {
            return new SinaAuthorizationCode();
        }

        public static IAuthorizationCodeBase Tencent()
        {
            return new TencentAuthorizationCode();
        }
    }
}
