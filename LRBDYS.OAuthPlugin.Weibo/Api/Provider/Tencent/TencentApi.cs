using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Common;
using LRBDYS.OAuthPlugin.Weibo.Config;
using LRBDYS.OAuthPlugin.Weibo.Interface;
using LRBDYS.OAuthPlugin.Weibo.JSON;

namespace LRBDYS.OAuthPlugin.Weibo.Api.Provider.Tencent
{
    public class TencentApi : IApi
    {
        public const string _apiUrl = "https://open.t.qq.com/api/";
        private static AppConfig _appConfig = AppConfigs.GetTencent();

        #region IApi 成员

        #region GET数据

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnJson"></param>
        /// <param name="paramsExt"></param>
        /// <returns></returns>
        public dynamic CallGet(string url, string accessToken, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            var openid = paramsExt["openid"].ToString();

            return Call(url, WebRequestMethods.Http.Get, accessToken, openid, null, null, returnJson);
        }

        #endregion

        #region POST数据

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="formData"></param>
        /// <param name="returnJson"></param>
        /// <param name="paramsExt"></param>
        /// <returns></returns>
        public dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            var openid = paramsExt["openid"].ToString();

            return Call(url, WebRequestMethods.Http.Post, accessToken, openid, formData, null, returnJson);
        }

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
        public dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, IDictionary<string, BinaryData> binaryData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            var openid = paramsExt["openid"].ToString();

            return Call(url, WebRequestMethods.Http.Post, accessToken, openid, formData, binaryData, returnJson);
        }

        #endregion

        #region HandlerError 错误处理

        /// <summary>
        /// 是否是错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool IsError<T>(dynamic obj, out T error) where T : class, IError
        {
            bool result = false;

            if (!IsError(obj))
            {
                error = default(T);
            }
            else
            {
                if (typeof(T) != typeof(TencentError))
                {
                    throw new Exception("泛型T的类型必须为 LRBDYS.OAuthPlugin.Weibo.Api.Provider.Tencent.TencentError");
                }

                var err = new TencentError();
                err.ErrorCode = obj.errcode;
                err.ret = obj.ret;
                err.msg = obj.msg;
                err.data = obj.data;

                error = err as T;
                result = true;
            }

            return result;
        }

        private bool IsError(dynamic obj)
        {
            if (obj.ret == null)
            {
                return false;
            }
            else
            {
                return obj.ret != 0;
            }

        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public T HandlerException<T>(Exception ex, System.Web.HttpContextBase context) where T : class, IError
        {
            var webEx = ex as WebException;

            if (ex == null)
            {
                throw ex;
            }

            var err = new TencentError();

            var json = string.Empty;

            using (var stream = webEx.Response.GetResponseStream())
            {
                using (var readerStream = new StreamReader(stream, Encoding.UTF8))
                {
                    json = readerStream.ReadToEnd();
                }
            }

            var errObj = JsonQuick.Deserialize(json);
            err.ErrorCode = errObj.errcode;
            err.ret = errObj.ret;
            err.msg = errObj.msg;
            err.data = errObj.data;

            T result = err as T;

            return result;
        }

        #endregion

        #endregion

        #region 自定义方法

        private dynamic Call(string url, string httpMethod, string accessToken, string openid, IDictionary<string, string> formData, IDictionary<string, BinaryData> binaryData, bool returnJson = false)
        {
            var linkChar = url.IndexOf('?') > 0 ? "&" : "?";

            var queryStringAccessToken = linkChar + "access_token=" + accessToken;
            var queryStringOAuthConsumerKey = "&oauth_consumer_key=" + _appConfig.AppKey;
            var queryStringOAuthVersion = "&oauth_version=2.a&scope=all";
            var queryStringOpenid = "&openid=" + openid;

            ClientRequest cr = new ClientRequest(_apiUrl + url + queryStringAccessToken + queryStringOAuthConsumerKey + queryStringOAuthVersion + queryStringOpenid);
            cr.HttpMethod = httpMethod;

            if (formData != null)
            {
                cr.FormData = formData;
                cr.ContentType = "application/x-www-form-urlencoded";
            }
            if (binaryData != null)
            {
                cr.BinaryData = binaryData;
                cr.ContentType = "multipart/form-data";
            }
            dynamic result = null;
            if (returnJson)
            {
                result = NetQuick.GetResponseForText(cr);
            }
            else
            {
                result = NetQuick.GetResponseForDynamic(cr);
            }
            return result;
        }

        #endregion

    }
}
