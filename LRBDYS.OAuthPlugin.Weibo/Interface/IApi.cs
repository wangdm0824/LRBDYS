using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LRBDYS.OAuthPlugin.Weibo.Common;

namespace LRBDYS.OAuthPlugin.Weibo.Interface
{
    public interface IApi
    {

        #region Get数据

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnJson"></param>
        /// <param name="paramsExt"></param>
        /// <returns></returns>
        dynamic CallGet(string url, string accessToken, bool returnJson = false, IDictionary<string, object> paramsExt = null);

        #endregion

        #region Post数据

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="formData"></param>
        /// <param name="returnJson"></param>
        /// <param name="paramsExt"></param>
        /// <returns></returns>
        dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="formData"></param>
        /// <param name="binaryData"></param>
        /// <param name="returnJson"></param>
        /// <param name="paramsExt"></param>
        /// <returns></returns>
        dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, IDictionary<string, BinaryData> binaryData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null);

        #endregion

        #region HandlerError 错误处理

        /// <summary>
        /// 是否是错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool IsError<T>(dynamic obj, out T error) where T : class, IError;

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        T HandlerException<T>(Exception ex, HttpContextBase context) where T : class, IError;

        #endregion

    }
}
