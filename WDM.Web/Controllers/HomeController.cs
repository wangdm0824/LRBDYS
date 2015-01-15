using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRBDYS.OAuthPlugin.Weibo;
using LRBDYS.OAuthPlugin.Weibo.AccessToken;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace WDM.Web.Controllers
{
    /// <summary>
    /// 申请AppKey和AppSecret
    /// 新浪微博的AppKey,AppSecret申请时会验证你是否拥有域名的所有权 ,腾讯则没有这个要求
    /// </summary>
    public class HomeController : Controller
    {
        //获取新浪、腾讯 IAuthorizationCodeBase 接口实例
        private IAuthorizationCodeBase sina = AccessTokenFactory.Create(CreateAT.Sina);
        private IAuthorizationCodeBase tencent = AccessTokenFactory.Create(CreateAT.Tencent);

        public ActionResult Index()
        {
            if (Session["AccessToken"] != null)
            {
                ViewBag.AccessTokenObj = Session["AccessToken"] as dynamic;
            }

            dynamic model = new ExpandoObject();

            //生成主机头
            //例如：http://www.baidu.com:8081   (注：默认80端口则不会显示:80)
            var hostPath = AccessTokenToolkit.GenerateHostPath(Request.Url);

            //定义授权成功后返回的url地址
            //var sinaRedirectUrl = hostPath + Url.Action("Index", "Sina");
            var tencentRedirectUrl = hostPath + Url.Action("Index", "Tencent");

            //设置超链接
            //model.SinaLink = sina.GenerateCodeUrl(sinaRedirectUrl);
            model.TencentLink = tencent.GenerateCodeUrl(tencentRedirectUrl);

            return View(model);
        }

        public ActionResult Exit()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
