using System.Threading.Tasks;
using Wechaty.Domain.Shared.DTO;

namespace Wechaty.Application.Contracts
{
    public interface IWechatyBotService : IWechatyHandlerService
    {
        //string Login_WeiXinId { get; set; }


        #region AppService
        IMessageAppService Message();
        IContactAppService Contact();
        IRoomAppService Room();
        ITagAppService Tag();
        IFavoriteAppService Favorite();
        IImageAppService Image();
        IFriendshipAppService Friendship();
        IRoomInvitationAppService RoomInvitation();
        IUrlLinkAppService UrlLink();
        #endregion


        void Instance(WechatyOptions options);
        Task Start();
        Task Stop();
    }
}
