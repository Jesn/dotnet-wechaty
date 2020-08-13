using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity.PlugIns;
using Volo.Abp.Threading;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.Domain.Shared.DTO;
using Wechaty.PlugIn;
using Wechaty.PuppetModel;

namespace Wechaty.Getting.Start
{

    public class ConsoleClientHostedService : IHostedService
    {
        private IWechatyBotService bot { get; set; }
        private IConfiguration configuration { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<WechatyConsoleModule>(options =>
              {
                  options.UseAutofac();
                  options.Services.AddLogging(loggingBuilder =>
                  {
                      loggingBuilder.AddSerilog(dispose: true);
                  });

                  // 绑定插件，方式一：加载模块 方式二：加载 dll
                  // TODO 这里可以读取所有继承 WechatyPluginBaseModule的模块，动态读取
                  options.PlugInSources.AddTypes(
                      typeof(WechatyDingDongModule),
                      typeof(WechatyQRCodeTerminalModule));

              }))
            {
                application.Initialize();

                configuration = application.ServiceProvider.GetRequiredService<IConfiguration>();


                WechatyOptions wechatyOptions = new WechatyOptions()
                {
                    Token = configuration["Wechaty_Token"],
                    Name = configuration["Wechaty_Name"],
                    EndPoint = configuration["Wechaty_EndPoint"]
                };

                // Bo
                bot = application.ServiceProvider.GetRequiredService<IWechatyBotService>();

                // 插件服务注册
                //var plugin = application.ServiceProvider.GetRequiredService<WechatyPluginBaseModule>();
                var qrCodePlugin = application.ServiceProvider.GetRequiredService<QRCodeTerminalAppService>();
                var dingdongPlugin = application.ServiceProvider.GetRequiredService<DingDongAppService>();

                bot.Instance(wechatyOptions);

                // Scan Event Plugin
                qrCodePlugin.QRCodeTerminalAsAscii();

                // Message Event Plugin
                dingdongPlugin.DingDong();

                // 直接监听
                bot.On<EventMessagePayload>(EventEnum.Message, async (eventData) =>
                {
                    var Message = bot.Message();
                    var payload = await Message.Ready(eventData.MessageId);

                    if (payload.Text == "天王盖地虎")
                    {
                        await Message.Say("宝塔镇河妖", payload);
                    }
                });

                await bot.Start();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await bot.Stop();
            Console.WriteLine("shutdown");
            // 彻底释放资源
            Process.GetCurrentProcess().Kill();
        }
    }
}
