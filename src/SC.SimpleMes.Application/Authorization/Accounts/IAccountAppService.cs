using System.Threading.Tasks;
using Abp.Application.Services;
using SC.SimpleMes.Authorization.Accounts.Dto;

namespace SC.SimpleMes.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

