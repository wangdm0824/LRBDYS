using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRBDYS.OAuthPlugin.QQ.Context;

namespace WDM.Web.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 数据上下文
        /// </summary>
        QQContext context = new QQContext();

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["RequestState"] = state;
            string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            var authenticationUrl = context.GetCodeUrl(state, scope);
            ViewBag.QQLogin = authenticationUrl;
            return View();
        }


        //参考文章
        //http://blog.csdn.net/xiaoxian8023/article/details/38114921
        //http://www.cnblogs.com/shanyou/archive/2012/02/05/2338797.html
    }
}
