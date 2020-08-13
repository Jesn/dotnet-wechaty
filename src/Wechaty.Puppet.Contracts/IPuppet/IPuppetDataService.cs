using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetContracts
{
    public partial interface IPuppetDataService : ITransientDependency
    {
        string selfId { get; set; }

        void Instance(PuppetOptions options);

        Task Start();
        Task Stop();
        Task LogOut();
        void Ding(string data = "");
    }
}
