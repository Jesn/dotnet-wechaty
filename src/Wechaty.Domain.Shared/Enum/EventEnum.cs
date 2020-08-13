using System;
using System.Collections.Generic;
using System.Text;

namespace Wechaty.Domain.Shared
{
    public enum EventEnum
    {
        Scan,
        Ready,
        Login,
        Heartbeat,
        Dong,
        Reset,
        Message,
        Friendship,
        RoomInvite,
        RoomJoin,
        RoomLeave,
        RoomTopic,
        Logout,
        Error,
    }
}
