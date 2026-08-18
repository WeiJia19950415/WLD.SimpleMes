using System.Threading.Tasks;
using Abp.Application.Services;
using SC.SimpleMes.Sessions.Dto;

namespace SC.SimpleMes.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

