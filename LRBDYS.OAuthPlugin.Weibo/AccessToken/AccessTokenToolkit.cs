using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LRBDYS.OAuthPlugin.Weibo.AccessToken
{
    public class AccessTokenToolkit
    {

        /// <summary>
        /// 生成主机地址路径如 http://www.baidu.com:8080
        /// (注：默认80端口则不会显示:80)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GenerateHostPath(HttpRequest request)
        {
            return GenerateHostPath(request.Url);
        }

        /// <summary>
        /// 生成主机地址路径如 http://www.baidu.com:8080
        /// (注：默认80端口则不会显示:80)
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GenerateHostPath(Uri uri)
        {
            var result = uri.Scheme + Uri.SchemeDelimiter + uri.Host + (uri.Port != 80 ? ":" + uri.Port : "");
            return result;
        }

    }
}
