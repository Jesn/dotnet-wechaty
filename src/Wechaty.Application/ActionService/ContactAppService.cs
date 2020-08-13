using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;
using Wechaty.PuppetModel.Filter;

namespace Wechaty.Application.ActionService
{
    public class ContactAppService : AccessoryAppService, IContactAppService
    {
        private readonly IDistributedCache<ContactPayload> _cacheContactPayload;
        public ContactPayload ContactPayload { get; set; }


        public ContactAppService(IDistributedCache<ContactPayload> cacheContactPayload)
        {
            _cacheContactPayload = cacheContactPayload;
        }


        public async Task Ready(string contactId, bool forceSync = false)
        {
            if (!forceSync && IsReady)
            {
                Logger.LogInformation("ready() isReady() true");
                return;
            }
            try
            {
                await ContactPayloadDirty(contactId);
                ContactPayload = await LoadContactPayload(contactId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "contact ready() failed");
            }
        }

        public async Task Ready()
        {
            await Ready(ContactPayload.Id);
        }

        protected async Task ContactPayloadDirty(string contactId)
        {
            await _cacheContactPayload.RemoveAsync(contactId);
        }

        private async Task<ContactPayload> LoadContactPayload(string contactId)
        {
            Check();

            var payload = await _cacheContactPayload.GetOrAddAsync(contactId,
                async () => await _puppetService.ContactRawPayload(contactId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
                });
            return payload;
        }

        public async Task<List<IContactAppService>> LoadAll(IReadOnlyList<string> contactIdList)
        {
            var list_contact = new List<IContactAppService>();

            foreach (var id in contactIdList)
            {
                var contact = await LoadForService(id);
                if (ContactPayload != null)
                {
                    list_contact.Add(contact);
                }
            }
            return list_contact;
        }

        protected async Task<IContactAppService> LoadForService(string id)
        {
            Check();

            ContactPayload = await _cacheContactPayload.GetOrAddAsync(id,
               async () => await _puppetService.ContactRawPayload(id),
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
               });
            return this;
        }

        async Task<IContactAppService> IContactAppService.Load(string id)
        {
            return await LoadForService(id);
        }


        public async Task<IContactAppService> LoadInfo(string id)
        {
            ContactPayload = await _cacheContactPayload.GetOrAddAsync(id,
              async () => await _puppetService.ContactRawPayload(id),
              () => new DistributedCacheEntryOptions
              {
                  AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
              });
            return this;
        }



        public async Task<ContactPayload> LoadSelf()
        {
            Check();

            if (LoginWeiXinId != _puppetService.selfId)
            {
                Logger.LogWarning($" user id not equit, wechaty login id is {LoginWeiXinId}, puppet id is {_puppetService.selfId}");
            }

            var self = await LoadContactPayload(LoginWeiXinId);
            return self;
        }

        #region Find

        // TODO 待完善
        public void Find(ContactQueryFilter query)
        {
            var contactList = FindAll(query);

        }


        public async Task<IReadOnlyList<IContactAppService>> FindAll(ContactQueryFilter queryFilter = default)
        {
            Check();
            try
            {
                var contactIdList = await ContactSearch(queryFilter);
                var list_contact = await LoadAll(contactIdList);
                return list_contact;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "contact findAll() exception");
                return null;
            }
        }


        protected async Task<IReadOnlyList<string>> ContactSearch(ContactQueryFilter query, params string[] searchIdList)
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
                    var payload = await ContactPayloadAsync(id);
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

        public async Task<ContactPayload> ContactPayloadAsync(string contactId)
        {
            //if (string.IsNullOrWhiteSpace(contactId))
            //{
            //    throw new ArgumentException("no id");
            //}
            //var cachedPayload = ContactPayloadCache(contactId);
            //if (cachedPayload != null)
            //{
            //    return cachedPayload;
            //}

            //var rawPayload = await _puppetService.ContactRawPayload(contactId);
            //var payload = _puppetService.ContactRawPayloadParser(rawPayload);

            //await _cacheContactPayload.SetAsync(contactId, payload,
            //    new DistributedCacheEntryOptions()
            //    {
            //        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
            //    });

            //return payload;

            var payload = await LoadContactPayload(contactId);
            return payload;
        }


        #endregion

        // TODO Contact Delete
        public async Task Delete()
        {

        }

        // TODO 待完善
        public async Task<List<string>> Tags()
        {
            Check();

            //var tagIdList = await _puppetService.TagContactList(ContactPayload.Id);
            var tagIdList = await Tag.Load(ContactPayload.Id);

            return tagIdList;
        }

        #region 方法
        public async Task SetAlias(string newAlias = default)
        {
            Check();
            try
            {
                await _puppetService.ContactAlias(ContactPayload.Id, newAlias);
                _cacheContactPayload.Remove(ContactPayload.Id);
                var payload = await LoadContactPayload(ContactPayload.Id);
                if (payload.Alias != newAlias)
                {
                    Logger.LogWarning($"alias({newAlias}) sync with server fail: set({newAlias}) is not equal to get({payload.Alias})");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"alias({newAlias}) rejected");
            }
        }
        public async Task<FileBox> Avatar()
        {
            try
            {
                var filebox = await _puppetService.ContactAvatar(ContactPayload.Id);
                return filebox;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Avatar() error");
                return null;
            }
        }



        #endregion

        #region 属性
        public bool IsReady => string.IsNullOrWhiteSpace(ContactPayload?.Name);
        public bool IsSelf => ContactPayload.Id == _puppetService.selfId;
        public string Name => ContactPayload?.Name;
        public ContactType Type => ContactPayload.Type;
        public string Alias => ContactPayload?.Alias;
        public bool? IsFriend => ContactPayload.Friend;
        public bool Personal => ContactPayload?.Type == ContactType.Individual;
        public bool? Star => ContactPayload?.Star;
        public ContactGender Gender => ContactPayload.Gender;
        public string Province => ContactPayload?.Province;
        public string City => ContactPayload?.City;
        public string Id => ContactPayload.Id;
        public string WeiXin => ContactPayload?.Weixin;




        #endregion

    }
}
