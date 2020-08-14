using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService
    {
        public async Task<RoomPayload> RoomRawPayload(string id)
        {
            var roomPayload = new RoomPayload();

            var request = new RoomPayloadRequest()
            {
                Id = id
            };

            var response = await grpcClient.RoomPayloadAsync(request);
            if (response != null)
            {
                roomPayload = new RoomPayload
                {
                    AdminIdList = response.AdminIds.ToList(),
                    Avatar = response.Avatar,
                    Id = response.Id,
                    MemberIdList = response.MemberIds.ToList(),
                    OwnerId = response.OwnerId,
                    Topic = response.Topic
                };
            }
            return roomPayload;
        }

        public RoomPayload RoomRawPayloadParser(RoomPayload payload)
        {
            return payload;
        }

        public async Task<List<string>> RoomList()
        {
            var response = await grpcClient.RoomListAsync(new RoomListRequest());
            return response?.Ids.ToList();
        }

        public async Task RoomDel(string roomId, string contactId)
        {
            var request = new RoomDelRequest()
            {
                ContactId = contactId,
                Id = roomId
            };
            await grpcClient.RoomDelAsync(request);
        }

        public async Task<FileBox> RoomAvatar(string roomId)
        {
            var request = new RoomAvatarRequest()
            { Id = roomId };

            var response = await grpcClient.RoomAvatarAsync(request);
            return JsonConvert.DeserializeObject<FileBox>(response?.Filebox);
        }

        public async Task RoomAdd(string roomId, string contactId)
        {
            var request = new RoomAddRequest()
            {
                ContactId = contactId,
                Id = roomId
            };
            await grpcClient.RoomAddAsync(request);
        }

        public async Task<string> RoomTopic(string roomId, string topic = "")
        {
            var request = new RoomTopicRequest()
            {
                Id = roomId
            };
            if (!string.IsNullOrEmpty(topic))
            {
                request.Topic = topic;
            }

            var response = await grpcClient.RoomTopicAsync(request);
            return response?.Topic;
        }

        public async Task<string> RoomCreate(List<string> contactIdList, string topic)
        {
            var request = new RoomCreateRequest();

            request.ContactIds.AddRange(contactIdList);
            request.Topic = topic;

            var response = await grpcClient.RoomCreateAsync(request);
            return response?.Id;
        }

        public async Task RoomQuit(string roomId)
        {
            var request = new RoomQuitRequest()
            {
                Id = roomId
            };
            await grpcClient.RoomQuitAsync(request);
        }

        public async Task<string> RoomQRCode(string roomId)
        {
            var request = new RoomQRCodeRequest()
            {
                Id = roomId
            };
            var response = await grpcClient.RoomQRCodeAsync(request);
            return response?.Qrcode;
        }

        public async Task<List<string>> RoomMemberList(string roomId)
        {
            var request = new RoomMemberListRequest()
            {
                Id = roomId
            };

            var response = await grpcClient.RoomMemberListAsync(request);
            return response?.MemberIds.ToList();
        }

        public async Task<RoomMemberPayload> RoomMemberRawPayload(string roomId, string contactId)
        {
            var payload = new RoomMemberPayload();

            var request = new RoomMemberPayloadRequest()
            {
                Id = roomId,
                MemberId = contactId
            };
            var response = await grpcClient.RoomMemberPayloadAsync(request);
            if (response != null)
            {
                payload = new RoomMemberPayload()
                {
                    Avatar = response.Avatar,
                    Id = response.Id,
                    InviterId = response.InviterId,
                    Name = response.Name,
                    RoomAlias = response.RoomAlias
                };
            }
            return payload;
        }

        public RoomMemberPayload RoomMemberRawPayloadParser(RoomMemberPayload payload)
        {
            return payload;
        }

        public async Task<string> RoomAnnounce(string roomId, string text = "")
        {
            var request = new RoomAnnounceRequest();
            request.Id = roomId;
            if (!string.IsNullOrEmpty(text))
            {
                request.Text = text;
            }

            var response = await grpcClient.RoomAnnounceAsync(request);
            return response?.Text;
        }

        public async Task RoomInvitationAccept(string roomInvitationId)
        {
            var request = new RoomInvitationAcceptRequest()
            {
                Id = roomInvitationId
            };
            await grpcClient.RoomInvitationAcceptAsync(request);
        }

        public async Task<RoomInvitationPayload> RoomInvitationRawPayload(string id)
        {
            var payload = new RoomInvitationPayload();
            var request = new RoomInvitationPayloadRequest()
            {
                Id = id
            };
            var response = await grpcClient.RoomInvitationPayloadAsync(request);

            if (response == null)
            {
                payload = new RoomInvitationPayload()
                {
                    Avatar = response.Avatar,
                    Id = response.Id,
                    Invitation = response.Invitation,
                    InviterId = response.InviterId,
                    MemberCount = response.MemberCount,
                    MemberIdList = response.MemberIds.ToList(),
                    ReceiverId = response.ReceiverId,
                    Timestamp = (long)response.Timestamp,
                    Topic = response.Topic
                };
            }
            return payload;
        }

        public RoomInvitationPayload RoomInvitationRawPayloadParser(RoomInvitationPayload payload)
        {
            return payload;
        }




    }
}
