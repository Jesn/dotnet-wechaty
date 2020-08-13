using System;
using System.Collections.Generic;
using System.Text;
using Wechaty.Application.Contracts;

namespace Wechaty.Application.Event
{
    public class RoomTopicActionData
    {
        public IRoomAppService Room { get; set; }
        public IRoomAppService Changer { get; set; }
        public DateTime Date { get; set; }
        public string OldTopic { get; set; }
        public string NewTopic { get; set; }
    }
}
