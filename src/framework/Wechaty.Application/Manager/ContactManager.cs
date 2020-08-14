using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Wechaty.PuppetContracts;
using Wechaty.PuppetModel;
using Wechaty.PuppetModel.Filter;

namespace Wechaty.Application.Manager
{
    public class ContactManager
    {
        private IPuppetDataService _puppetService;
        private IDistributedCache<ContactPayload> _cacheContactPayload;

        public ContactManager(IPuppetDataService puppetService, IDistributedCache<ContactPayload> cacheContactPayload)
        {
            _puppetService = puppetService;
            _cacheContactPayload = cacheContactPayload;
        }

        protected async Task<IReadOnlyList<string>> ContactSearch(ContactQueryFilter? query, params string[] searchIdList)
        {
            if (searchIdList == null)
            {
                searchIdList = (await _puppetService.ContactList()).ToArray();
            }
            
            if (query == null)
            {
                return searchIdList;
            }
            var filterFuncion = query.Every<ContactQueryFilter, ContactPayload>();

            const int BATCH_SIZE = 16;
            var batchIndex = 0;

            var resultIdList = new List<string>();
            while (BATCH_SIZE * batchIndex < searchIdList.Length)
            {
                var batchSearchIdList = searchIdList.Skip(
                  BATCH_SIZE * batchIndex
                ).Take(BATCH_SIZE);

                var matchBatchIdFutureList = batchSearchIdList.Select(Matcher);
                var matchBatchIdList = await Task.WhenAll(matchBatchIdFutureList.ToArray());

                var batchSearchIdResultList = matchBatchIdList.Where(t => !string.IsNullOrWhiteSpace(t));

                resultIdList.AddRange(batchSearchIdResultList);

                batchIndex++;
            }
       

            return resultIdList;

            async Task<string> Matcher(string id)
            {
                try
                {
                    /**
                     * Does LRU cache matter at here?
                     */
                    // const rawPayload = await this.contactRawPayload(id)
                    // const payload    = await this.contactRawPayloadParser(rawPayload)
                    var payload = await ContactPayload(id);
                    if (payload != null && filterFuncion(payload))
                    {
                        return id;
                    }
                }
                catch (Exception exception)
                {
                    //if (Logger.IsEnabled(LogLevel.Trace))
                    //{
                    //    Logger.LogTrace(exception, $"contactSearch() contactPayload failed.");
                    //}
                    
                    await ContactPayloadDirty(id);
                }
                return null;
            }
        }

        protected ContactPayload? ContactPayloadCache(string contactId)
        {
            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentException("no id");
            }
            var cachedPayload = _cacheContactPayload.Get(contactId);

            return cachedPayload;
        }

        public async Task<ContactPayload> ContactPayload(string contactId)
        {
            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentException("no id");
            }
            var cachedPayload = ContactPayloadCache(contactId);
            if (cachedPayload!=null)
            {
                return cachedPayload;
            }

            var rawPayload =await _puppetService.ContactRawPayload(contactId);
            var payload = _puppetService.ContactRawPayloadParser(rawPayload);

            await _cacheContactPayload.SetAsync(contactId, payload);

            return payload;
        }

        protected async Task ContactPayloadDirty(string contactId)
        {
            await _cacheContactPayload.RemoveAsync(contactId);
        }


        public async Task FindAll(ContactQueryFilter? query)
        {
            try
            {
                var contactIdList =await ContactSearch(query);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
