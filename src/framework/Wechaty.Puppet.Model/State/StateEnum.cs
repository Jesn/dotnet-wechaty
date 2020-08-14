using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Wechaty.PuppetModel
{
    public enum StateEnum
    {
        [Description("Pending")]
        Pending = 1,

        [Description("On")]
        On = 2,

        [Description("Off")]
        Off = 3
    }
}
