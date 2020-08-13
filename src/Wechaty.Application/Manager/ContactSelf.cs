using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Manager
{
    /*
     * 所有的操作完成后，都必须要更新缓存信息
     */
    public class ContactSelf : AccessoryAppService
    {
        public Task<FileBox> Avatar(FileBox file)
        {
            var filebox = _puppetService.ContactAvatar(LoginWeiXinId, file);
            return filebox;
        }

        public async Task<string> QrCode()
        {
            var qrCodeValue =await _puppetService.ContactSelfQRCode();
            return qrCodeValue;
        }

        public async Task SetName(string name)
        {
            try
            {
                await _puppetService.ContactSelfName(name);
            }
            catch (Exception ex)
            {
                throw new BusinessException("set self new name is exception",innerException:ex);
            }
        }

        public async Task SetSignature(string signature)
        {
            try
            {
                await _puppetService.ContactSelfSignature(signature);
            }
            catch (Exception ex)
            {
                throw new BusinessException("set self signature is exception", innerException: ex);
            }
        }



    }
}
