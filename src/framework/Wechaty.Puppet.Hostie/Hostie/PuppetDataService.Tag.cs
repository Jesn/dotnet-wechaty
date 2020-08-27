using github.wechaty.grpc.puppet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService
    {
        public async Task TagContactAdd(string id, string contactId)
        {
            var request = new TagContactAddRequest()
            {
                Id = id,
                ContactId = contactId
            };
            await grpcClient.TagContactAddAsync(request);
        }

        public async Task TagContactRemove(string id, string contactId)
        {
            var request = new TagContactRemoveRequest()
            {
                Id = id,
                ContactId = contactId
            };

            await grpcClient.TagContactRemoveAsync(request);
        }

        public async Task TagContactDelete(string id)
        {
            var request = new TagContactDeleteRequest()
            {
                Id = id
            };

            await grpcClient.TagContactDeleteAsync(request);
        }

        public async Task<List<string>> TagContactList(string contactId = "")
        {
            var request = new TagContactListRequest();
            if (!string.IsNullOrEmpty(contactId))
            {
                request.ContactId = contactId;
            }

            var response = await grpcClient.TagContactListAsync(request);
            return response.Ids.ToList();
        }
    }
}
