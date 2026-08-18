using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.WorkStation
{
    /// <summary>
    /// 产线信息管理服务
    /// </summary>
    public interface IProductLineAppService : IAsyncCrudAppService<ProductLineDto, long, CommonPageRequestDto, ProductLineDto, ProductLineDto>
    {
        /// <summary>
        /// 绑定员工与工序关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> BingUserAndWorkProcessAsync(TransferDto dto);

        /// <summary>
        /// 获取所有用户信息与工序绑定的用户  --用于前端Transfer组件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TransferDto GetUserListAndBingUser(EntityDto dto);

        /// <summary>
        /// 获取管理的产线
        /// </summary>
        /// <returns></returns>
        Task<List<ProductLineDto>> GetMangedProductLinesAsync();
    }
}
