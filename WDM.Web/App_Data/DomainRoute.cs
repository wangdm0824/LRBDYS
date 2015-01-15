using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
/*
 * 二级域名的实现类（来自网上的方法，自己添加了一个写入DataTokens中的功能）
 * Author：Jobily
 * Email:jobily@foxmail.com
 * UpdateTime：2011-08-19 10:45
 */
namespace Ezhiol.Common
{
    public class DomainRoute : Route
    {

        #region 数据成员

        private Regex domainRegex;
        private Regex pathRegex;

        #endregion

        #region 属性

        public string Domain { get; set; }

        #endregion

        #region 构造函数

        public DomainRoute(string domain, string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler())
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults, object constraints, IRouteHandler routeHandler)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), routeHandler)
        {
            Domain = domain;
        }

        #endregion

        #region 自定义函数

        /// <summary>
        /// 请求->判断IP地址所在的地区,生成路径->过滤路径->转发处理
        /// </summary>
        /// <param name="httpContext">Context对象</param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {

            // 构造 regex
            domainRegex = CreateRegex(Domain);
            pathRegex = CreateRegex(Url);

            // 请求信息
            string requestDomain = httpContext.Request.Headers["host"];

            if (!string.IsNullOrEmpty(requestDomain) && requestDomain.IndexOf(":") > 0)
                requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
            else
                requestDomain = httpContext.Request.Url.Host;

            string requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
            requestPath = requestPath.Trim('/');

            //判断如果是~/course/0或者~/school/0形式则默认加上action(index)
            requestPath = CreateNewUrl(requestPath);

            // 匹配域名和路由
            Match domainMatch = domainRegex.Match(requestDomain);
            Match pathMatch = pathRegex.Match(requestPath);

            // 路由数据
            RouteData data = null;
            if (domainMatch.Success && pathMatch.Success)
            {
                data = new RouteData(this, RouteHandler);

                // 添加默认选项
                if (Defaults != null)
                {
                    foreach (KeyValuePair<string, object> item in Defaults)
                    {
                        data.Values[item.Key] = item.Value;
                        if (item.Key.Equals("cityName") || item.Key.Equals("Namespaces"))
                            data.DataTokens[item.Key] = item.Value;
                    }
                }

                // 匹配域名路由
                for (int i = 1; i < domainMatch.Groups.Count; i++)
                {
                    Group group = domainMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = domainRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0)
                            && !string.IsNullOrEmpty(group.Value))
                        {
                            data.Values[key] = group.Value;
                            if (key.Equals("cityName")) data.DataTokens[key] = group.Value;
                        }
                    }
                }

                #region 匹配域名路径

                for (int i = 1; i < pathMatch.Groups.Count; i++)
                {
                    Group group = pathMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = pathRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0) && !string.IsNullOrEmpty(group.Value))
                        {
                            data.Values[key] = group.Value;
                            if (key.Equals("cityName")) data.DataTokens[key] = group.Value;
                        }
                    }
                }

                #endregion
            }
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, RemoveDomainTokens(values));
        }

        public DomainData GetDomainData(RequestContext requestContext, RouteValueDictionary values)
        {
            // 获得主机名
            string hostname = Domain;
            foreach (KeyValuePair<string, object> pair in values)
            {
                hostname = hostname.Replace("{" + pair.Key + "}", pair.Value.ToString());
            }

            // Return 域名数据
            return new DomainData
            {
                Protocol = "http",
                HostName = hostname,
                Fragment = ""
            };
        }

        private Regex CreateRegex(string source)
        {
            // 替换
            source = source.Replace("/", @"\/?");
            source = source.Replace(".", @"\.?");
            source = source.Replace("-", @"\-?");
            source = source.Replace("{", @"(?<");
            source = source.Replace("}", @">([0-9a-zA-Z\u4e00-\u9fa5]*))");

            return new Regex("^" + source + "$");
        }

        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
        {
            Regex tokenRegex = new Regex(@"({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?");
            Match tokenMatch = tokenRegex.Match(Domain);
            for (int i = 0; i < tokenMatch.Groups.Count; i++)
            {
                Group group = tokenMatch.Groups[i];
                if (group.Success)
                {
                    string key = group.Value.Replace("{", "").Replace("}", "");
                    if (values.ContainsKey(key))
                        values.Remove(key);
                }
            }
            return values;
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="num></param>
        /// <returns></returns>
        private bool CheckIsNum(string num)
        {
            int a = 0;
            return int.TryParse(num, out a);
        }

        /// <summary>
        /// 课程列表和机构列表默认增加index的作为action
        /// </summary>
        /// <param name="url">旧url</param>
        /// <returns></returns>
        private string CreateNewUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            url = url.ToLower().Trim();
            string newUrl = "/" + url;
            string courseReg = "/course/";
            string schoolReg = "/school/";

            if (newUrl.IndexOf(courseReg) >= 0 || newUrl.IndexOf(schoolReg) >= 0)
            {
                int index = newUrl.IndexOf(courseReg);
                if (index < 0) index = newUrl.IndexOf(schoolReg);

                string temp = url.Substring(index + 7, 1);
                if (CheckIsNum(temp))
                    url = url.Insert(index + 7, "index/");
            }
            return url;
        }

        #endregion

    }

    /// <summary>
    /// 路由对象
    /// </summary>
    public class DomainData
    {
        public string Protocol { get; set; }
        public string HostName { get; set; }
        public string Fragment { get; set; }
    }

}
