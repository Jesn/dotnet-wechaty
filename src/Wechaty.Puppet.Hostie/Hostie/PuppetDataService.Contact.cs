using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService
    {
        public async Task<string> ContactAlias(string contactId, string alias = "")
        {
            var request = new ContactAliasRequest();
            if (!string.IsNullOrEmpty(alias))
            {
                request.Alias = alias;
            }
            request.Id = contactId;

            var response = await grpcClient.ContactAliasAsync(request);

            return response.Alias;
        }

        public async Task<List<string>> ContactList()
        {
            var response = await grpcClient.ContactListAsync(new ContactListRequest());
            return response?.Ids.ToList();
        }

        public async Task<string> ContactQRCode(string contactId)
        {
            if (contactId != selfId)
            {
                //throw new UserFriendlyException("can not set avatar for others");
                throw new BusinessException("can not set avatar for others");
            }
            var response = await grpcClient.ContactSelfQRCodeAsync(new ContactSelfQRCodeRequest());
            return response?.Qrcode;
        }

        public async Task<FileBox> ContactAvatar(string contactId, FileBox file = null)
        {
            var fileBox = new FileBox();

            var request = new ContactAvatarRequest();
            request.Id = contactId;

            if (file != null)
            {
                request.Filebox = JsonConvert.SerializeObject(file);
            }
            var response = await grpcClient.ContactAvatarAsync(request);
            if (response != null && !string.IsNullOrEmpty(response.Filebox))
            {
                fileBox = JsonConvert.DeserializeObject<FileBox>(response.Filebox);
            }
            return fileBox;
        }

        public async Task<ContactPayload> ContactRawPayload(string id)
        {
            var payload = new ContactPayload();

            var request = new ContactPayloadRequest();
            request.Id = id;

            var response = await grpcClient.ContactPayloadAsync(request);

            if (response != null)
            {
                payload = new ContactPayload()
                {
                    Address = response.Address,
                    Alias = response.Alias,
                    Avatar = response.Avatar,
                    City = response.City,
                    Friend = response.Friend,
                    Gender = (PuppetModel.ContactGender)response.Gender,
                    Id = response.Id,
                    Name = response.Name,
                    Province = response.Province,
                    Signature = response.Signature,
                    Star = response.Star,
                    Type = (PuppetModel.ContactType)response.Type,
                    Weixin = response.Weixin,
                };
            }
            return payload;
        }

        public ContactPayload ContactRawPayloadParser(ContactPayload payload)
        {
            return payload;
        }

        public async Task ContactSelfName(string name)
        {
            var request = new ContactSelfNameRequest();
            await grpcClient.ContactSelfNameAsync(request);
        }

        public async Task<string> ContactSelfQRCode()
        {
            var response = await grpcClient.ContactSelfQRCodeAsync(new ContactSelfQRCodeRequest());
            return response?.Qrcode;
        }

        public async Task ContactSelfSignature(string signature)
        {
            var request = new ContactSelfSignatureRequest();
            request.Signature = signature;

            await grpcClient.ContactSelfSignatureAsync(request);
        }

    }
}
