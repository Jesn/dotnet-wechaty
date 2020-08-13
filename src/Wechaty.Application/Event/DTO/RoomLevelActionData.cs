using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Wechaty.Application.Contracts;

namespace Wechaty.Application.Event
{
    public class RoomLevelActionData
    {
        public IRoomAppService Room { get; set; }
        public List<IContactAppService> LeaverList { get; set; }
        public IContactAppService Remover { get; set; }
    }
}
