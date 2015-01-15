using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.QQ.Models
{
    /// <summary>
    /// QQ OAuth2.0
    /// </summary>
    public class OAuthToken
    {
        /// <summary>
        /// access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpiresAt { get; set; }

    }
}
