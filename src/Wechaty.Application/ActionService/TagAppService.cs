using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Wechaty.Application.Contracts;

namespace Wechaty.Application.ActionService
{
    public class TagAppService : AccessoryAppService, ITagAppService
    {
        public TagAppService()
        {

        }

        public async Task<List<string>> Load(string contactId=default)
        {
            var list_tagContact =await _puppetService.TagContactList(contactId);
            return list_tagContact;
        }



        public async Task Add(string contactId,string tagId)
        {
            await _puppetService.TagContactAdd(tagId, contactId);
        }

        public async Task Remove(string contactId,string tagId)
        {
            await _puppetService.TagContactRemove(tagId, contactId);
        }

        public async Task Delete(string contactId)
        {
            await _puppetService.TagContactDelete(contactId);
        }



    }
}
