using System.Collections.Generic;
using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetContracts
{
    public partial interface IPuppetDataService
	{
		Task<string> ContactAlias(string contactId, string alias = "");
		Task<List<string>> ContactList();
		Task<string> ContactQRCode(string contactId);
		Task<FileBox> ContactAvatar(string contactId, FileBox file = null);
		Task<ContactPayload> ContactRawPayload(string id);
		ContactPayload ContactRawPayloadParser(ContactPayload payload);
		Task ContactSelfName(string name);
		Task<string> ContactSelfQRCode();
		Task ContactSelfSignature(string signature);
	}
}
