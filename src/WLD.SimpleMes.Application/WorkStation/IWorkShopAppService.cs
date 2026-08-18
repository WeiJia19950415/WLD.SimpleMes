using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.WorkStation
{
    public interface IWorkShopAppService: IAsyncCrudAppService<WorkShopInfoDto, long, CommonPageRequestDto, WorkShopInfoDto, WorkShopInfoDto>
    {
    }
}
