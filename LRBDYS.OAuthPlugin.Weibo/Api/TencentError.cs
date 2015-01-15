using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRBDYS.OAuthPlugin.Weibo.Interface;

namespace LRBDYS.OAuthPlugin.Weibo.Api
{
    public class TencentError : IError
    {
        public int ret { get; set; }

        #region IError 成员

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        #endregion

        public string msg { get; set; }
        public dynamic data { get; set; }

        public override string ToString()
        {
            var result = string.Empty;

            var data = string.Empty;
            data = (this.data == null) ? "null" : this.data.ToString();

            result = string.Format("{{\"data\":{0},\"errcode\":{1},\"msg\":\"{2}\",\"ret\":{3}}}", data, ErrorCode.ToString(), msg, ret.ToString());

            return result;
        }
    }
}
/*
 *  Example
    {
        data: null,
        detailerrinfo: {
            accesstoken: "",
            apiname: "weibo.t.show",
            appkey: "",
            clientip: "119.255.26.231",
            cmd: 0,
            proctime: 0,
            ret1: 20,
            ret2: 1,
            ret3: 20,
            ret4: 2300806410,
            timestamp: 1346219805
        },
        errcode: 20,
        msg: "error id param",
        ret: 1,
        seqid: 5781970035711489000
    }
 * ret为一级错误码
 * errcode为二级错误码
 * 
 * http://wiki.open.t.qq.com/index.php/%E8%BF%94%E5%9B%9E%E9%94%99%E8%AF%AF%E7%A0%81%E8%AF%B4%E6%98%8E
 * http://wiki.open.t.qq.com/index.php/%E9%94%99%E8%AF%AF%E7%A0%81%E8%AF%B4%E6%98%8E
 */
