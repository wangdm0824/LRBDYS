using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LRBDYS.OAuthPlugin.QQ.Config;
using LRBDYS.OAuthPlugin.QQ.Context;
using LRBDYS.OAuthPlugin.QQ.Exceptions;
using RestSharp;
using RestSharp.Deserializers;

namespace LRBDYS.OAuthPlugin.QQ.Api
{
    /// <summary>
    /// QQ登录API
    /// </summary>
    public partial class RestApi
    {

        private RestClient _restClient;
        private RequestHelper _requestHelper;

        /// <summary>
        /// QQ互联API的上下文数据
        /// </summary>
        private QQContext context;
        public QQContext Context
        {
            get { return context; }
            set { context = value; }
        }

        /// <summary>
        /// 构造函数，初始化访问QQ互联API的上下文数据
        /// </summary>
        /// <param name="context"></param>
        public RestApi(QQContext context)
        {
            this.context = context;
            this._requestHelper = new RequestHelper();
            _restClient = new RestClient(LRBDYS.OAuthPlugin.QQ.Config.EndPoint.ApiBaseUrl);
        }

        #region 私有辅助方法

        private void ExecuteAsync(RestRequest request, Action<IRestResponse> success, Action<QQException> failure)
        {
            _restClient.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    failure(new QQException(response));
                }
                else
                {
                    success(response);
                }
            });
        }

        private IRestResponse Execute(RestRequest request)
        {
            var response = _restClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new QQException(response);
            }
            return response;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        private T Deserialize<T>(string content) where T : new()
        {
            var restResponse = new RestResponse { Content = content };
            var d = new JsonDeserializer();
            var payload = d.Deserialize<T>(restResponse);
            return payload;
        }

        #endregion

    }
}
