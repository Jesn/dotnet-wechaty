using OpenGraphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wechaty.Application.Contracts;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class UrlLinkAppService : AccessoryAppService, IUrlLinkAppService
    {
        public async Task<UrlLinkPayload> Create(string url)
        {
            var openGraph = await OpenGraph.ParseUrlAsync(url);
            return new UrlLinkPayload
            {
                Url = openGraph.Url.AbsoluteUri,
                Title = openGraph.Title,
                ThumbnailUrl = openGraph.Image?.AbsoluteUri,
                Description = openGraph.Metadata["description"].FirstOrDefault()?.Value
            };

        }
    }
}
