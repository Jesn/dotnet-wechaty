using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Wechaty.Application.Contracts;

namespace Wechaty.Application.Event
{
    public class RoomJoinActionData
    {
        public IRoomAppService Room { get; set; }
        public List<IContactAppService> InviteedList { get; set; }
        public IContactAppService Inviter { get; set; }
        public DateTime Date { get; set; }
    }
}
