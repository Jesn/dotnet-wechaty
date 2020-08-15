using github.wechaty.grpc.puppet;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;
using Wechaty.Domain.Shared;
using Wechaty.PuppetContracts;
using Wechaty.PuppetModel;
using static Wechaty.Puppet;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService : IPuppetDataService
    {
        private ILogger<PuppetDataService> Logger { get; set; }
        public string selfId { get; set; }

        private IDistributedEventBus _localEventBus;

        private PuppetClient grpcClient = null;
        private Grpc.Net.Client.GrpcChannel channel = null;
        //private Channel channel = null;
        private PuppetOptions puppetOptions = null;

        private StateEnum state = StateEnum.Off;

        public PuppetDataService(IDistributedEventBus localEventBus)
        {
            _localEventBus = localEventBus;
            Logger = NullLogger<PuppetDataService>.Instance;
        }


        public void Instance(PuppetOptions options)
        {
            puppetOptions = new PuppetOptions()
            {
                EndPoint = options.EndPoint,
                Timeout = options.Timeout == 0 ? 60000 : options.Timeout,
                Token = options.Token,
                Name = options.Name
            };
        }

        /// <summary>
        /// 发现 hostie gateway 对应的服务是否能能访问
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected async Task<HostieEndPoint> DiscoverHostieIp(string token)
        {
            try
            {
                var url = WechatyPuppetConst.CHATIE_ENDPOINT + token;

                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var model = JsonConvert.DeserializeObject<HostieEndPoint>(await response.Content.ReadAsStringAsync());
                        return model;
                    }
                }
                throw new BusinessException("获取hostie gateway 对应的主机信息异常");
            }
            catch (Exception ex)
            {
                throw new BusinessException("获取hostie gateway 对应的主机信息异常");
            }
        }

        /// <summary>
        /// 初始化 Grpc连接
        /// </summary>
        /// <returns></returns>
        protected async Task StartGrpcClient()
        {
            try
            {
                if (grpcClient != null)
                {
                    throw new BusinessException("puppetClient had already inited");
                }

                var endPoint = puppetOptions.EndPoint;
                if (string.IsNullOrEmpty(endPoint))
                {
                    var model = await DiscoverHostieIp(puppetOptions.Token);
                    if (model.IP == "0.0.0.0" || model.Port == "0")
                    {
                        throw new Exception("no endpoint");
                    }
                    // 方式一
                    endPoint = "http://" + model.IP + ":" + model.Port;

                    puppetOptions.EndPoint = endPoint;

                    // 方式二
                    //endPoint = model.IP + ":" + model.Port;
                }

                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

                // 方式一
                channel = Grpc.Net.Client.GrpcChannel.ForAddress(endPoint);
                grpcClient = new PuppetClient(channel);

                // 方式二
                //channel = new Channel(endPoint, ChannelCredentials.Insecure);
                //grpcClient = new PuppetClient(channel);

                //try
                //{
                //    var version = grpcClient.Version(new VersionRequest()).Version;

                //    //var resonse = grpcClient.Ding(new DingRequest() { Data = "ding" });

                //    //await channel.ShutdownAsync();
                //}
                //catch (Exception ex)
                //{

                //}
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.StackTrace);
            }
        }

        /// <summary>
        /// 关闭Grpc连接
        /// </summary>
        /// <returns></returns>
        protected async Task StopGrpcClient()
        {
            if (channel == null || grpcClient == null)
            {
                throw new Exception("puppetClient had not initialized");
            }
            await channel.ShutdownAsync();
            grpcClient = null;
        }


        /// <summary>
        /// 双向数据流事件处理
        /// </summary>
        protected void StartGrpcStream()
        {
            try
            {
                var eventStream = grpcClient.Event(new EventRequest());

                var stream = Task.Run(async () =>
                {
                    await foreach (var resp in eventStream.ResponseStream.ReadAllAsync())
                    {
                        await OnGrpcStreamEvent(resp);
                    }
                });
            }
            catch (Exception ex)
            {
                EventResetPayload eventResetPayload = new EventResetPayload()
                {
                    Data = ex.StackTrace
                };
                _localEventBus.PublishAsync(eventResetPayload);
            }
        }

        protected async Task OnGrpcStreamEvent(EventResponse _event)
        {
            try
            {
                EventType eventType = _event.Type;
                string payload = _event.Payload;

                Console.WriteLine($"{eventType},PayLoad:{payload}");

                if (eventType != EventType.Heartbeat)
                {
                    EventHeartbeatPayload eventHeartbeatPayload = new EventHeartbeatPayload()
                    {
                        Data = $"onGrpcStreamEvent({eventType.ToString()})"
                    };
                    await _localEventBus.PublishAsync(eventHeartbeatPayload);
                }

                switch (eventType)
                {
                    case EventType.Unspecified:
                        Logger.LogError("onGrpcStreamEvent() got an EventType.EVENT_TYPE_UNSPECIFIED ?");
                        break;
                    case EventType.Heartbeat:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventHeartbeatPayload>(payload));
                        break;
                    case EventType.Message:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventMessagePayload>(payload));
                        break;
                    case EventType.Dong:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventDongPayload>(payload));
                        break;
                    case EventType.Error:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventErrorPayload>(payload));
                        break;
                    case EventType.Friendship:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<FriendshipPayload>(payload));
                        break;
                    case EventType.RoomInvite:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventRoomInvitePayload>(payload));
                        break;
                    case EventType.RoomJoin:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventRoomJoinPayload>(payload));
                        break;
                    case EventType.RoomLeave:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventRoomLeavePayload>(payload));
                        break;
                    case EventType.RoomTopic:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventRoomTopicPayload>(payload));
                        break;
                    case EventType.Scan:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventScanPayload>(payload));
                        break;
                    case EventType.Ready:
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventReadyPayload>(payload));
                        break;
                    case EventType.Reset:
                        //await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventResetPayload>(payload));
                        //log.warn('PuppetHostie', 'onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?')
                        // the `reset` event should be dealed not send out
                        Logger.LogWarning("onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?");
                        break;
                    case EventType.Login:
                        var loginPayload = JsonConvert.DeserializeObject<EventLoginPayload>(payload);
                        selfId = loginPayload.ContactId;
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventLoginPayload>(payload));
                        break;
                    case EventType.Logout:
                        selfId = string.Empty;
                        await _localEventBus.PublishAsync(JsonConvert.DeserializeObject<EventLogoutPayload>(payload));
                        break;
                        //default:
                        //    Console.WriteLine($"'eventType {_event.Type.ToString()} unsupported! (code should not reach here)");

                        //    //throw new BusinessException($"'eventType {_event.Type.ToString()} unsupported! (code should not reach here)");
                        //    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Logger.LogError(ex, "OnGrpcStreamEvent exception");
            }

        }
        protected void StopGrpcStream()
        {
            _localEventBus.UnsubscribeAll(typeof(LocalEventBus));
        }


        #region interface implementation
        public async Task Start()
        {
            try
            {
                if (puppetOptions.Token == "")
                {
                    throw new Exception("wechaty-puppet-hostie: token not found. See: <https://github.com/wechaty/wechaty-puppet-hostie#1-wechaty_puppet_hostie_token>");
                }
                if (state == StateEnum.On)
                {
                    Logger.LogWarning("start() is called on a ON puppet. await ready(on) and return.");

                    state = StateEnum.On;
                    return;
                }

                state = StateEnum.Pending;

                if (channel == null)
                {
                    await StartGrpcClient();
                }
                StartGrpcStream();

                await grpcClient.StartAsync(new StartRequest());

                state = StateEnum.On;
            }
            catch (Exception ex)
            {
                state = StateEnum.Off;

                Logger.LogError(ex.StackTrace);
                throw new BusinessException(ex.StackTrace);
            }
        }

        public async Task Stop()
        {
            if (state == StateEnum.Off)
            {
                Logger.LogWarning("stop() is called on a OFF puppet. await ready(off) and return.");
                state = StateEnum.Off;
                return;
            }

            try
            {
                state = StateEnum.Pending;

                if (LoginInfo.LogOnOff())
                {
                    EventLogoutPayload logoutPayload = new EventLogoutPayload()
                    {
                        ContactId = selfId,
                        Data = "stop() this.grpcClient not exist"
                    };
                    await _localEventBus.PublishAsync(logoutPayload);
                    selfId = string.Empty;
                }

                StopGrpcStream();

                if (grpcClient != null)
                {
                    await grpcClient.StopAsync(new StopRequest());
                }

                await StopGrpcClient();

                // 取消所有订阅
                _localEventBus.UnsubscribeAll(typeof(LocalEventBus));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.StackTrace);
                throw new BusinessException(ex.StackTrace);
            }
            finally
            {
                state = StateEnum.Off;
            }
        }

        public async Task LogOut()
        {
            if (selfId == string.Empty)
            {
                //throw new BusinessException("logout before login?");
                Logger.LogWarning("logout before login ?");
            }

            try
            {
                await grpcClient.LogoutAsync(new LogoutRequest());
            }
            catch (Exception ex)
            {
                Logger.LogError($"loginout() exception :{ex.StackTrace}");
            }
        }

        public void Ding(string data = "")
        {
            var request = new DingRequest();
            request.Data = data;

            grpcClient.Ding(request);

        }


        #endregion




    }
}
