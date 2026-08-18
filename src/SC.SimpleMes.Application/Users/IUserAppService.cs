using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using SC.SimpleMes.Roles.Dto;
using SC.SimpleMes.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SC.SimpleMes.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<JHTAjaxResponse> ResetPassWord(ResetPasswordDto input);
        void BatchToggleUserActiveState(long[] userIds);

        void DeActivate(EntityDto<long> user);
        void Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<IdentityResult> UpdateUserBasicInfoAsync(UpdateUserDto updateUserDto);

        Task<string> SaveHeadImageAsync(IFormFile file, long userId = 0);
        /// <summary>
        /// ��ҳ��ѯ�û��б�
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<ViewUserDto>> SearchUserIncludeRole(JHTPageAjaxResquest<UserConditionDto> where);

        Task<PageData<UserDto>> SearchUser(JHTPageAjaxResquest<UserConditionDto> where);
        /// <summary>
        /// ���ݵ绰�����ȡΨһ���û�
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        Task<UserDto> FindPhoneNumberAsync(int? tenantId, string phoneNumber);
        /// <summary>
        /// �����û��һָ���ɾ�����û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> UpdateAndResumeAsync(UserDto input);
        /// <summary>
        /// ��ȡΨһ���û�
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        Task<UserDto> FindNameOrEmailAddressAsync(int? tenantId, string userName, string emailAddress);
        /// <summary>
        /// ��˾����û�
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<ResultDto<bool>> AddUserAsync(UserDto userDto);
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        UserDto GetUserDto(long Id);
        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultDto<bool>> UpdateUser(UserDto dto);

        /// <summary>
        /// 获取用户对应的工位Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<List<long>> GetWorkStationUserRelationIds(EntityDto<long> dto);
    }
}

