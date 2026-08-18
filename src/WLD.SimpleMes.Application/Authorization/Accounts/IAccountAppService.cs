using System.Threading.Tasks;
using Abp.Application.Services;
using WLD.SimpleMes.Authorization.Accounts.Dto;

namespace WLD.SimpleMes.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

