using System.Threading.Tasks;
using Abp.Application.Services;
using WLD.SimpleMes.Sessions.Dto;

namespace WLD.SimpleMes.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

