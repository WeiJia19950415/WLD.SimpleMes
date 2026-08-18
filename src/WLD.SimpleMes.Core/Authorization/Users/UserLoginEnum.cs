using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Authorization.Users
{
    public class UserLoginEnum
    {
        public enum UserLoginInfoEnum
        {
            [Description("微信小程序")]
            WeChatMini,
            [Description("微信Unionid")]
            WeChatUniId,
            [Description("海康威视")]
            HaiKangVideo,
        }
    }
}

