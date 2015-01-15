using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;

namespace LRBDYS.OAuthPlugin.QQ.Exceptions
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public class QQException : Exception
    {
        /// <summary>
        /// Http请求状态
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The response of the error call (for Debugging use)
        /// </summary>
        public IRestResponse Response { get; private set; }

        /// <summary>
        ///默认构造函数
        /// </summary>
        public QQException()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public QQException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="r"></param>
        public QQException(IRestResponse r)
        {
            Response = r;
            StatusCode = r.StatusCode;
        }

    }
}
