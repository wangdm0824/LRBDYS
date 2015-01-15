using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    public interface IError
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        int ErrorCode { get; set; }
    }
}
