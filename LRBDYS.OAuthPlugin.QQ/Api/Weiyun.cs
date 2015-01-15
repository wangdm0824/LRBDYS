using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.QQ.Authenticators;
using LRBDYS.OAuthPlugin.QQ.Models;

namespace LRBDYS.OAuthPlugin.QQ.Api
{
    /// <summary>
    /// 微云api
    /// </summary>
    public partial class RestApi
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        public void UploadFile()
        {

        }

        /// <summary>
        /// 获取库视图支持的类型
        /// </summary>
        /// <param name="appId">第三方接入ID</param>         
        /// <returns></returns>
        public GetMediaTypeResult GetLibraryType()
        {
            _restClient.Authenticator = new OAuthUriQueryParameterAuthenticator(context.AccessToken.OpenId, context.AccessToken.AccessToken, context.Config.GetAppKey());
            var request = _requestHelper.CreateGetLibraryTypeRequest(context.Config.GetAppKey());
            var response = Execute(request);
            var payload = Deserialize<GetMediaTypeResult>(response.Content);
            return payload;
        }
    }
}
