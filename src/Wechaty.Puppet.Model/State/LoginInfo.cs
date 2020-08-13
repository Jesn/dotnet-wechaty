using System;
using System.Collections.Generic;
using System.Text;

namespace Wechaty.PuppetModel
{
    public class LoginInfo
    {
        public static string selfId = string.Empty;

        public static bool LogOnOff()
        {
            return selfId != string.Empty;
        }


    }
}
