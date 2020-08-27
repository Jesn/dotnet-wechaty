using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wechaty.PuppetContracts
{
    public  partial interface IPuppetDataService
    {
     Task TagContactAdd(string id, string contactId);
     Task TagContactRemove(string id, string contactId);
     Task TagContactDelete(string id);
     Task<List<string>> TagContactList(string contactId = "");
    }
}
