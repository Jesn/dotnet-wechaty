using System.Collections.Generic;
using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetContracts
{
    public partial interface IPuppetDataService
    {
        Task<RoomPayload> RoomRawPayload(string id);
        RoomPayload RoomRawPayloadParser(RoomPayload payload);
        Task<List<string>> RoomList();
        Task RoomDel(string roomId, string contactId);
        Task<FileBox> RoomAvatar(string roomId);
        Task RoomAdd(string roomId, string contactId);
        Task<string> RoomTopic(string roomId, string topic = "");
        Task<string> RoomCreate(List<string> contactIdList, string topic);
        Task RoomQuit(string roomId);
        Task<string> RoomQRCode(string roomId);
        Task<List<string>> RoomMemberList(string roomId);
        Task<RoomMemberPayload> RoomMemberRawPayload(string roomId, string contactId);
        RoomMemberPayload RoomMemberRawPayloadParser(RoomMemberPayload payload);
        Task<string> RoomAnnounce(string roomId, string text = "");
        Task RoomInvitationAccept(string roomInvitationId);
        Task<RoomInvitationPayload> RoomInvitationRawPayload(string id);
        RoomInvitationPayload RoomInvitationRawPayloadParser(RoomInvitationPayload payload);
    }
}
