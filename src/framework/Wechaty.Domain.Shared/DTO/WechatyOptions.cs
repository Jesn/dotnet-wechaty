using System;
using System.Collections.Generic;
using System.Text;

namespace Wechaty.Domain.Shared.DTO
{
    public class WechatyOptions
    {
        public string EndPoint { get; set; }
        public int Timeout { get; set; } = 60000;
        public string Token { get; set; }
        public string Name { get; set; } = "Csharp-Wechatty";
    }
}
