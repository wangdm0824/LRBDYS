using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.Api
{
    public class SinaError : IError
    {
        public string error { get; set; }
        public string request { get; set; }

        #region IError 成员

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        #endregion

        public override string ToString()
        {
            var result = string.Empty;

            result = string.Format("{{\"error\":\"{0}\",\"error_code\":{1},\"request\":\"{2}\"}}", error, ErrorCode.ToString(), request);

            return result;
        }

    }
}

/*
 *  Example: 
    {
        error: "source paramter(appkey) is missing",
        error_code: 10006,
        request: "/2/users/show.json"
    }
 * 
 * 20502 -> 2 05 02
 *  2 = 服务级错误（1为系统级错误）
 * 05 = 服务模块代码
 * 02 = 具体错误代码
 * 
 * http://open.t.sina.com.cn/wiki/Error_code
 */

