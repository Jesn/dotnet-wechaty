using QRCoder;
using System;
using System.Threading.Tasks;
using Wechaty.Domain.Shared;
using Wechaty.Plugin.Base;
using Wechaty.PuppetModel;
using static QRCoder.PayloadGenerator;

namespace Wechaty.PlugIn
{
    public class QRCodeTerminalAppService : PlugInApplicationService
    {
        public void QRCodeTerminalAsAscii()
        {
            WechahtyEvent.Subscribe<EventScanPayload>(async (eventData) =>
            {
                if (eventData.Status == ScanStatus.Waiting || eventData.Status == ScanStatus.Timeout)
                {
                    string qrcodeImageUrl = GetWechatGithubQrCode(eventData.Qrcode);

                    Url generator = new Url(qrcodeImageUrl);
                    string payload = generator.ToString();

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L);

                    AsciiQRCode qrCodeAsi = new AsciiQRCode(qrCodeData);
                    string qrCodeAsAsciiArt = qrCodeAsi.GetGraphic(1);
                    Console.WriteLine(qrCodeAsAsciiArt);
                    
                }
            });
        }

        public async Task QRCodeTerminalAsAscii(string qrCode)
        {
            string qrcodeImageUrl = GetWechatGithubQrCode(qrCode);

            Url generator = new Url(qrcodeImageUrl);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L);

            AsciiQRCode qrCodeAsi = new AsciiQRCode(qrCodeData);
            string qrCodeAsAsciiArt = qrCodeAsi.GetGraphic(1);
            Console.WriteLine(qrCodeAsAsciiArt);

            await Message.Say("Hello this is come from plugin ");
        }

        public string QRCodeTerminalAsBase64(string qrCode)
        {
            string qrcodeImageUrl = GetWechatGithubQrCode(qrCode);

            Url generator = new Url(qrcodeImageUrl);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L);

            Base64QRCode qrCodeBase64 = new Base64QRCode(qrCodeData);
            string qrCodeAsBase64 = qrCodeBase64.GetGraphic(1);
            return qrCodeAsBase64;
        }

        /// <summary>
        /// 把微信qrcode 装换为 wechaty对应的二维码
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        private string GetWechatGithubQrCode(string qrCode) => WechatyConst.QrcodeServerUrl + qrCode;

    }
}
