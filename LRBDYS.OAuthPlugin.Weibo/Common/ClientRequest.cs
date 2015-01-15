using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Common
{
    public class ClientRequest
    {
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// CookieContainer
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// Accept
        /// </summary>
        public string Accept { get; set; }
        /// <summary>
        /// 获取或设置 GetResponse 和 GetRequestStream 方法的超时值（毫秒）
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// Referer
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 获取或设置写入或读取流时的超时（以毫秒为单位）。
        /// </summary>
        public int ReadWriteTimeout { get; set; }
        /// <summary>
        /// ProtocolVersion
        /// </summary>
        public Version ProtocolVersion { get; set; }
        /// <summary>
        /// KeepAlive
        /// </summary>
        public bool KeepAlive { get; set; }
        /// <summary>
        /// IfModifiedSince
        /// </summary>
        public DateTime IfModifiedSince { get; set; }
        /// <summary>
        /// Expect
        /// </summary>
        public string Expect { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 浏览器类型版本
        /// </summary>
        string UserAgent { get; set; }
        /// <summary>
        /// FormData数据
        /// </summary>
        public IDictionary<string, string> FormData { get; set; }

        public ClientRequest(string url)
        {
            Url = url;
        }

        #region 文件上传

        /// <summary>
        /// 以post方式 上传带附件 的请求时使用
        /// </summary>
        public string Boundary { get; set; }
        public IDictionary<string, BinaryData> BinaryData { get; set; }

        #endregion

    }
}
