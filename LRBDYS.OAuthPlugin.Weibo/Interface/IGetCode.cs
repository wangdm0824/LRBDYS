using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    public interface IGetCode
    {
        /// <summary>
        /// 生成产生Code的地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        string GenerateCodeUrl(string redirectUrl);
    }
}
