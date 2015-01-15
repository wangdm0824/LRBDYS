using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Api.Provider.Sina;
using LRBDYS.OAuthPlugin.Weibo.Api.Provider.Tencent;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.Api
{
    public class CreateApi
    {
        public static IApi Sina()
        {
            return new SinaApi();
        }

        public static IApi Tencent()
        {
            return new TencentApi();
        }
    }
}
