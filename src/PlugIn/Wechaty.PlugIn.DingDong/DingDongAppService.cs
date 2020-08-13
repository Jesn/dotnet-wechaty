using Wechaty.Plugin.Base;
using Wechaty.PuppetModel;

namespace Wechaty.PlugIn
{
    public class DingDongAppService : PlugInApplicationService
    {
        public void DingDong()
        {
            WechahtyEvent.Subscribe<EventMessagePayload>(async (eventData) =>
            {
                var payload = await Message.Ready(eventData.MessageId);

                if (payload.Text.ToLower() == "ding")
                {
                    await Message.Say("dong");
                }
                else if (payload.Text.ToLower() == "dong")
                {
                    await Message.Say("ding");
                }
            });
        }

    }
}
