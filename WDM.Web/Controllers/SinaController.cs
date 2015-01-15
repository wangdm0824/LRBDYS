using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRBDYS.OAuthPlugin.Weibo;
using LRBDYS.OAuthPlugin.Weibo.AccessToken;
using LRBDYS.OAuthPlugin.Weibo.Api;
using LRBDYS.OAuthPlugin.Weibo.Common;
using LRBDYS.OAuthPlugin.Weibo.Interface;
using WDM.Web.Filters;

namespace WDM.Web.Controllers
{
    /// <summary>
    /// 暂未测试
    /// </summary>
    public class SinaController : Controller
    {
        private IAuthorizationCodeBase _authCode = AccessTokenFactory.Create(CreateAT.Sina);
        private SinaError _err = null;
        private IApi api = AccessTokenFactory.Create(CreateApi.Sina);

        public ActionResult Index(string code)
        {
            if (Session["accessToken"] != null)
            {
                return View(new object());
            }

            if (!string.IsNullOrEmpty(code))
            {
                var redirectUrl = AccessTokenToolkit.GenerateHostPath(Request.Url) + Url.Action("Index");

                var accessToken = _authCode.GetResult(_authCode.GenerateAccessTokenUrl(redirectUrl, code));

                if (api.IsError(accessToken, out _err))
                {
                    Session["err"] = _err;
                    return RedirectToAction("Error");
                }

                Session.Add("accessToken", accessToken);

                return View(new object());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AccessTokenRequired]
        public ActionResult ShowUserInfo()
        {
            var accessTokenObj = Session["accessToken"] as dynamic;
            var uid = accessTokenObj.uid;
            var accessToken = accessTokenObj.access_token;

            var model = api.CallGet("users/show.json?uid=" + uid, accessToken);

            if (api.IsError(model, out _err))
            {
                Session["err"] = _err;
                return RedirectToAction("Error");
            }

            return View(model);
        }

        [AccessTokenRequired]
        public ActionResult SendMsg()
        {
            var accessTokenObj = Session["accessToken"] as dynamic;
            var uid = accessTokenObj.uid;
            var accessToken = accessTokenObj.access_token;

            //msg:要发表的微博信息
            var msg = "新浪微博api接口测试   测试人：∮〆﹏落日般的忧伤づミ   时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var formData = new Dictionary<string, string>();
            formData.Add("status", Server.UrlEncode(msg));

            var result = api.CallPost("statuses/update.json", accessToken, formData, true);

            if (api.IsError(result, out _err))
            {
                Session["err"] = _err;
                return RedirectToAction("Error");
            }

            return View();
        }

        [AccessTokenRequired]
        public ActionResult SendMsgWithImg()
        {
            return View();
        }

        [HttpPost]
        [AccessTokenRequired]
        public ActionResult SendMsgWithImg(FormCollection form)
        {
            var accessTokenObj = Session["accessToken"] as dynamic;
            var uid = accessTokenObj.uid;
            string accessToken = accessTokenObj.access_token;

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("status", Url.Encode(Request.Form["status"]));

            Dictionary<string, BinaryData> binaryData = new Dictionary<string, BinaryData>();
            BinaryData bd = new BinaryData();
            bd.FileName = System.IO.Path.GetFileName(Request.Files["pic"].FileName);
            bd.ContentType = Request.Files["pic"].ContentType;

            var stream = Request.Files["pic"].InputStream;
            var readerStream = new System.IO.BinaryReader(stream);

            bd.Binary = readerStream.ReadBytes((int)stream.Length);

            binaryData.Add("pic", bd);

            var result = string.Empty;

            try
            {
                result = api.CallPost("statuses/upload.json", accessToken, formData, binaryData);
            }
            catch (System.Net.WebException wEx)
            {
                var srcStream = wEx.Response.GetResponseStream();

                var rStream = new System.IO.StreamReader(srcStream, System.Text.Encoding.UTF8);
                result = rStream.ReadToEnd();

            }




            return View((object)result);
        }

        public ActionResult Error()
        {
            var err = Session["err"] as SinaError;
            return View(err);
        }
    }
}
