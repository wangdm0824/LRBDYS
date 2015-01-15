using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRBDYS.OAuthPlugin.Weibo;
using LRBDYS.OAuthPlugin.Weibo.AccessToken;
using LRBDYS.OAuthPlugin.Weibo.Api;
using LRBDYS.OAuthPlugin.Weibo.Interface;
using WDM.Web.Filters;

namespace WDM.Web.Controllers
{
    public class TencentController : Controller
    {
        private IAuthorizationCodeBase tencent = AccessTokenFactory.Create(CreateAT.Tencent);
        private TencentError _err = null;
        private IApi api = AccessTokenFactory.Create(CreateApi.Tencent);

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="code"></param>
        /// <param name="openid"></param>
        /// <param name="openkey"></param>
        /// <returns></returns>
        public ActionResult Index(string code, string openid, string openkey)
        {
            if (Session["AccessToken"] != null)
            {
                return View(new object());
            }
            if (!string.IsNullOrEmpty(code))
            {
                var redirectUrl = AccessTokenToolkit.GenerateHostPath(Request.Url) + Url.Action("Index");

                var accessToken = tencent.GetResult(tencent.GenerateAccessTokenUrl(redirectUrl, code));

                if (api.IsError(accessToken, out _err))
                {
                    Session["err"] = _err;
                    return RedirectToAction("Error");
                }

                accessToken.openid = openid;               //注意腾讯微创新
                accessToken.openkey = openkey;             //注意腾讯微创新

                Session.Add("AccessToken", accessToken);

                return RedirectToAction("Index", "Home");
                //return View(new object());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <returns></returns>
        [AccessTokenRequired]
        public ActionResult ShowUserInfo()
        {
            var accessTokenObj = Session["AccessToken"] as dynamic;
            var accessToken = accessTokenObj.access_token;

            var model = api.CallGet("user/info?format=json", accessToken, false, GetOpenidOpenkeyParamsExt());

            if (api.IsError(model, out _err))
            {
                Session["err"] = _err;
                return RedirectToAction("Error");
            }

            return View(model.data);
        }

        /// <summary>
        /// 发表腾讯微博
        /// </summary>
        /// <returns></returns>
        [AccessTokenRequired]
        public ActionResult SendMsg()
        {
            var accessTokenObj = Session["accessToken"] as dynamic;
            var uid = accessTokenObj.name;
            var accessToken = accessTokenObj.access_token;
            var openid = accessTokenObj.openid;

            //msg:要发表的微博信息
            var msg = "腾讯微博api接口测试   测试人：∮〆﹏落日般的忧伤づミ   时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var formData = new Dictionary<string, string>();
            formData.Add("content", Server.UrlEncode(msg));

            var result = api.CallPost("t/add?format=json", accessToken, formData, false, GetOpenidOpenkeyParamsExt());
            if (api.IsError(result, out _err))
            {
                Session["err"] = _err;
                return RedirectToAction("Error");
            }
            return View();
        }

        /// <summary>
        /// 获得openid、opendkey的字段集合
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private IDictionary<string, object> GetOpenidOpenkeyParamsExt()
        {
            var result = new Dictionary<string, object>();

            var accessTokenObj = Session["accessToken"] as dynamic;
            var openid = accessTokenObj.openid;
            var openkey = accessTokenObj.openkey;

            result.Add("openid", openid);
            result.Add("openkey", openkey);

            return result;
        }

        //腾讯微博Autho2.0 API接口文档
        //http://wenku.baidu.com/link?url=dve-W9RDZwKbCR7EAy9yG08UyfPTqgxrtuv58ZQeFEqvCGXaxQMVX9Zu0mmMNYkxjUYXEXIcX7v7qGBHV2Aav1Z_ME_WzardjJUGsqCANsy

    }
}
