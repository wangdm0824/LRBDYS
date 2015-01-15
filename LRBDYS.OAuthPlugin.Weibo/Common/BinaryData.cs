using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LRBDYS.OAuthPlugin.Weibo.Common
{
    /// <summary>
    /// 二进制数据
    /// </summary>
    public class BinaryData
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Binary { get; set; }
    }
}
