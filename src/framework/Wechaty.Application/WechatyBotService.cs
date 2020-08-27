using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Wechaty.Application.ActionService;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.Domain.Shared.DTO;
using Wechaty.PuppetContracts;
using Wechaty.PuppetModel;

namespace Wechaty.Application
{
    public class WechatyBotService : AccessoryAppService, IWechatyBotService
    {
        public new  ILogger<WechatyBotService> Logger { get; set; }

        public WechatyBotService(IPuppetDataService puppetService,
            ContactAppService _contact,
            MessageAppService _message,
            RoomAppService _room,
            TagAppService _tag,
            FavoriteAppService _favorite,
            ImageAppService _image,
            FriendshipAppService _friendship,
            RoomInvitationAppService _roomInvitation,
            UrlLinkAppService _urlLink
            )
        {
            _puppetService = puppetService;
            Logger = NullLogger<WechatyBotService>.Instance;

            Contact = _contact;
            Message = _message;
            Room = _room;
            Tag = _tag;
            Favorite = _favorite;
            Image = _image;
            Friendship = _friendship;
            RoomInvitation = _roomInvitation;
            UrlLink = _urlLink;
        }

        public void Instance(WechatyOptions options)
        {
            if (options.Token.IsNullOrEmpty())
            {
                throw new UserFriendlyException("token is empty,please set token!");
            }
            _puppetService.Instance(new PuppetOptions()
            {
                Name = options.Name,
                Token = options.Token,
                EndPoint = options.EndPoint,
                Timeout = options.Timeout
            });
        }

        #region Action Init
        IMessageAppService IWechatyBotService.Message()
        {
            return Message;
        }
        IContactAppService IWechatyBotService.Contact()
        {
            return Contact;
        }
        IRoomAppService IWechatyBotService.Room()
        {
            return Room;
        }

        ITagAppService IWechatyBotService.Tag()
        {
            return Tag;
        }

        IFavoriteAppService IWechatyBotService.Favorite()
        {
            return Favorite;
        }

        IImageAppService IWechatyBotService.Image()
        {
            return Image;
        }

        IFriendshipAppService IWechatyBotService.Friendship()
        {
            return Friendship;
        }

        IRoomInvitationAppService IWechatyBotService.RoomInvitation()
        {
            return RoomInvitation;
        }

        IUrlLinkAppService IWechatyBotService.UrlLink()
        {
            return UrlLink;
        }
        public IWechatyBotService WechatBot()
        {
            return this;
        }
        #endregion

        public async Task Start()
        {
            await _puppetService.Start();
        }

        public async Task Stop()
        {
            readyState = StateEnum.Off;
            LoginWeiXinId = string.Empty;
            
            await _puppetService.Stop();
        }

        public void On<T>(EventEnum @event, Func<T, Task> func)
        {
            switch (@event)
            {
                case EventEnum.Scan:
                    WechahtyEvent.Subscribe(func as Func<EventScanPayload, Task>);
                    break;
                case EventEnum.Ready:
                    WechahtyEvent.Subscribe(func as Func<EventReadyPayload, Task>);
                    break;
                case EventEnum.Login:
                    WechahtyEvent.Subscribe(func as Func<EventLoginPayload, Task>);
                    break;
                case EventEnum.Heartbeat:
                    WechahtyEvent.Subscribe(func as Func<EventHeartbeatPayload, Task>);
                    break;
                case EventEnum.Dong:
                    break;
                case EventEnum.Reset:
                    break;
                case EventEnum.Message:
                    WechahtyEvent.Subscribe(func as Func<EventMessagePayload, Task>);
                    break;
                case EventEnum.Friendship:
                    break;
                case EventEnum.RoomInvite:
                    break;
                case EventEnum.RoomJoin:
                    break;
                case EventEnum.RoomLeave:
                    break;
                case EventEnum.RoomTopic:
                    break;
                case EventEnum.Logout:
                    break;
                case EventEnum.Error:
                    break;
                default:
                    break;
            }
        }

    }
}
