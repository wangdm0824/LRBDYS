using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Config
{
    /// <summary>
    /// web.config配置文件的 AppConfig对象设置
    /// </summary>
    public class AppConfigs
    {
        private const string _APP_KEY_NAME = ".weibo.appkey";
        private const string _APP_SECRET_NAME = ".weibo.appsecret";

        /// <summary>
        /// 获取指定微博的key, secret
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static AppConfig Get(string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentNullException("provider");
            }

            var result = new AppConfig();

            provider = provider.ToLower();

            result.AppKey = ConfigurationManager.AppSettings[provider + _APP_KEY_NAME];
            result.AppSecret = ConfigurationManager.AppSettings[provider + _APP_SECRET_NAME];

            if (result.AppKey == null)
            {
                throw new Exception("节点 appSettings 下 <add key=\"" + provider + _APP_KEY_NAME + "\" /> 没有被找到!");
            }

            if (result.AppSecret == null)
            {
                throw new Exception("节点 appSettings 下 <add key=\"" + provider + _APP_SECRET_NAME + "\" /> 没有被找到!");
            }

            return result;
        }

        /// <summary>
        /// 获取腾讯微博的key, secret
        /// </summary>
        /// <returns></returns>
        public static AppConfig GetTencent()
        {
            return Get(DefaultAppConfig.Tencent);
        }

        /// <summary>
        /// 获取新浪微博的key, secret
        /// </summary>
        /// <returns></returns>
        public static AppConfig GetSina()
        {
            return Get(DefaultAppConfig.Sina);
        }
    }
}
