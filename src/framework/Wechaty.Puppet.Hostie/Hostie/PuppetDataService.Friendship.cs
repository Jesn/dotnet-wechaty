using github.wechaty.grpc.puppet;
using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService
    {
        public async Task<string> FriendshipSearchPhone(string phone)
        {
            var request = new FriendshipSearchPhoneRequest()
            {
                Phone = phone
            };

            var response = await grpcClient.FriendshipSearchPhoneAsync(request);
            return response?.ContactId;
        }

        public async Task<string> FriendshipSearchWeixin(string weixin)
        {
            var request = new FriendshipSearchWeixinRequest()
            { Weixin = weixin };

            var respnse = await grpcClient.FriendshipSearchWeixinAsync(request);
            return respnse?.ContactId;
        }

        public async Task<FriendshipPayload> FriendshipRawPayload(string id)
        {
            var payload = new FriendshipPayload();

            var request = new FriendshipPayloadRequest()
            {
                Id = id
            };

            var response = await grpcClient.FriendshipPayloadAsync(request);
            if (response != null)
            {
                payload = new FriendshipPayload()
                {
                    ContactId = response.ContactId,
                    Hello = response.Hello,
                    Id = response.Id,
                    Scene = (int)response.Scene,
                    Stranger = response.Stranger,
                    Ticket = response.Ticket,
                    Type = (PuppetModel.FriendshipType)response.Type
                };
            }
            return payload;
        }

        public FriendshipPayload FriendshipRawPayloadParser(FriendshipPayload payload)
        {
            return payload;
        }

        public async Task FriendshipAdd(string contactId, string hello)
        {
            var request = new FriendshipAddRequest()
            {
                ContactId = contactId,
                Hello = hello
            };
            var response = await grpcClient.FriendshipAddAsync(request);


        }

        public async Task FriendshipAccept(string friendshipId)
        {
            var request = new FriendshipAcceptRequest()
            { Id = friendshipId };

            await grpcClient.FriendshipAcceptAsync(request);
        }

    }
}
