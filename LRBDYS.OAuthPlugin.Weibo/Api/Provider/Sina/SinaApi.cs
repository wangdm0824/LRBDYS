using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.Api.Provider.Sina
{
    public class SinaApi : IApi
    {
        public dynamic CallGet(string url, string accessToken, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            throw new NotImplementedException();
        }

        public dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            throw new NotImplementedException();
        }

        public dynamic CallPost(string url, string accessToken, IDictionary<string, string> formData = null, IDictionary<string, Common.BinaryData> binaryData = null, bool returnJson = false, IDictionary<string, object> paramsExt = null)
        {
            throw new NotImplementedException();
        }

        public bool IsError<T>(dynamic obj, out T error) where T : class, IError
        {
            throw new NotImplementedException();
        }

        public T HandlerException<T>(Exception ex, System.Web.HttpContextBase context) where T : class, IError
        {
            throw new NotImplementedException();
        }
    }
}
