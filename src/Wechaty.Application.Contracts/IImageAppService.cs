using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Contracts
{
    public interface IImageAppService : IApplicationService
    {
        Task<FileBox> Thumbnail(string messageId);
        Task<FileBox> HD(string messageId);
        Task<FileBox> Artwork(string messageId);

    }
}
