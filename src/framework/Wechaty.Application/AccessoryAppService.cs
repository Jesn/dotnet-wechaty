using System;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;
using Wechaty.Application.ActionService;
using Wechaty.PuppetContracts;
using Wechaty.PuppetModel;

namespace Wechaty.Application
{
    public class AccessoryAppService : ApplicationService
    {

        public  IDistributedEventBus WechahtyEvent { get; set; }
        public AccessoryAppService()
        {
            WechahtyEvent = NullDistributedEventBus.Instance;
        }

        #region Wechat Info
        public StateEnum readyState = StateEnum.Off;

        public static string LoginWeiXinId { get; set; }
        #endregion

        #region Action Service
        public static ContactAppService Contact { get; set; }
        public static MessageAppService Message { get; set; }
        public static RoomAppService Room { get; set; }
        public static TagAppService Tag { get; set; }
        public static FavoriteAppService Favorite { get; set; }
        public static ImageAppService Image { get; set; }
        public static FriendshipAppService Friendship { get; set; }
        public static RoomInvitationAppService RoomInvitation { get; set; }
        public static UrlLinkAppService UrlLink { get; set; }

        #endregion

        #region Puppet Info
        public static IPuppetDataService _puppetService;
        //public static WechatyOptions _wechatyOptions;
        #endregion


        public virtual void Check()
        {
            if (_puppetService == null || _puppetService.selfId.IsNullOrEmpty())
                throw new UserFriendlyException("puppet is null");
            else if (_puppetService.selfId.IsNullOrEmpty())
                throw new UserFriendlyException("puppet self is empty,please login");
            else if (LoginWeiXinId.IsNullOrEmpty())
                throw new UserFriendlyException("wechaty is not login");
            else if (_puppetService.selfId != LoginWeiXinId)
                throw new UserFriendlyException($"puppet self is {_puppetService.selfId}, wechaty login is {LoginWeiXinId},is not equal !");
            // TODO  添加 wechaty 状态判断
        }

    }
}
