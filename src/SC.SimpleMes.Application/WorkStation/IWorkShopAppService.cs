using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkStation
{
    public interface IWorkShopAppService: IAsyncCrudAppService<WorkShopInfoDto, long, CommonPageRequestDto, WorkShopInfoDto, WorkShopInfoDto>
    {
    }
}
