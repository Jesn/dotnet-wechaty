using System.Threading.Tasks;
using Wechaty.Application.Contracts;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class ImageAppService : AccessoryAppService, IImageAppService
    {
        public async Task<FileBox> Thumbnail(string messageId)
        {
            return await _puppetService.MessageImage(messageId, ImageType.Thumbnail);
        }

        public async Task<FileBox> HD(string messageId)
        {
            return await _puppetService.MessageImage(messageId, ImageType.HD);
        }

        public async Task<FileBox> Artwork(string messageId)
        {
            return await _puppetService.MessageImage(messageId, ImageType.Artwork);
        }

    }
}
