using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetContracts
{
    public partial interface IPuppetDataService
	{
	  Task<string> FriendshipSearchPhone(string phone);
	 Task<string> FriendshipSearchWeixin(string weixin);
	 Task<FriendshipPayload> FriendshipRawPayload(string id);
	 FriendshipPayload FriendshipRawPayloadParser(FriendshipPayload payload);
	 Task FriendshipAdd(string contactId, string hello);
	 Task FriendshipAccept(string friendshipId);
	}
}
