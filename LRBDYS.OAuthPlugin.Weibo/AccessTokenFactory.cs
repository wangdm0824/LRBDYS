using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo
{
    /// <summary>
    /// AccessTokenFactory
    /// </summary>
    public static class AccessTokenFactory
    {
        /// <summary>
        /// 创建接口的实例化
        /// </summary>
        /// <typeparam name="T">返回的接口类型</typeparam>
        /// <param name="createInstanceCallBack">具体创建接口实例化的函数</param>
        /// <returns></returns>
        public static T Create<T>(Func<T> createInstanceCallBack)
        {
            return createInstanceCallBack();
        }
    }
}
