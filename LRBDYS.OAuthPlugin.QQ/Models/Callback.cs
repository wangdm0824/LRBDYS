using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.QQ.Models
{
    /// <summary>
    /// 根据access_token获得对应用户身份的openid
    /// </summary>
    public class Callback
    {
        /// <summary>
        /// client_id
        /// </summary>
        public string client_id { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
    }
}
