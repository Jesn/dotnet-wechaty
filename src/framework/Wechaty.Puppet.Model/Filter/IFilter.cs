using System;
using System.Collections.Generic;
using System.Text;

namespace Wechaty.PuppetModel.Filter
{
    public interface IFilter
    {
        StringOrRegex? this[string key] { get; }
        IReadOnlyList<string> Keys { get; }
    }
}
