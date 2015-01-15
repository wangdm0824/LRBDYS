using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    /// <summary>
    /// OAuth 2.0 在国内支持的授权方式
    /// 1.Authorization Code方式
    /// 2.Resource Owner Password Credentials方式
    /// 3.Implicit Grant方式
    /// </summary>
    public interface IAuthorizationCodeBase : IGetCode, IGetAccessToken, IAccessToken
    {

    }
}
