using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using LRBDYS.OAuthPlugin.Weibo.Common;

namespace LRBDYS.OAuthPlugin.Weibo.JSON
{
    public static class JsonQuick
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic Deserialize(string json)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic result = serializer.Deserialize(json, typeof(object));

            return result;
        }

        /// <summary>
        /// 待测试
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public static string Serializer(dynamic jsonObj)
        {
            var result = string.Empty;

            if (jsonObj is DynamicDictionary)
            {
                result = jsonObj.ToString();
            }
            else
            {
                var serializer = new JavaScriptSerializer();
                //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                result = serializer.Serialize(jsonObj);
            }

            return result;
        }
    }
}
