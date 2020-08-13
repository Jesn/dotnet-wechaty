using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Wechaty.PuppetModel.schemas
{
    public class WechatEventDict
    {
        public static Dictionary<string, string> CHAT_EVENT_DICT = new Dictionary<string, string>()
        {
            {"friendship","receive a friend request" },
            { "login","puppet had logined"},
            {"logout","puppet had logouted" },
            {"message","received a new message" },
            {"room-invite","received a room invitation" },
            {"room-join","be added to a room" },
            {"room-leave","leave or be removed from a room" },
            {"scan","a QR Code scan is required" }
        };

        public static Dictionary<string, string> PUPPET_EVENT_DICT = new Dictionary<string, string>()
        {
            {"dong","emit this event if you received a ding() call"},
            {"error","emit an Error instance when there\"s any Error need to report to Wechaty"},
            {"heart-beat","feed the watchdog by emit this event"},
            {"ready","emit this event after the puppet is ready(you define it)"},
            {"reset","reset the puppet by emit this event"}
        };

        public static Dictionary<string, string> GetChatEventDict()
        {
            return CHAT_EVENT_DICT;
        }

        public static List<string> GetChatEventDictKyes()
        {
            return CHAT_EVENT_DICT.Keys.ToList();
        }


        public static Dictionary<string, string> GetPuppetEventDict()
        {
            foreach (var item in CHAT_EVENT_DICT)
            {
                PUPPET_EVENT_DICT.Add(item.Key, item.Value);
            }
            return PUPPET_EVENT_DICT;
        }

        public static List<string> GetPuppetEventDictKeys()
        {
            var dic = GetPuppetEventDict();
            return dic.Keys.ToList();
        }


    }
}

