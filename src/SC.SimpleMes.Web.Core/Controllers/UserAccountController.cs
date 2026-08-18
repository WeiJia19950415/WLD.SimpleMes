using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using AutoMapper;
using JHT.Abp.CommonModels;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.Models.TokenAuth;
using SC.SimpleMes.MultiTenancy;
using SC.SimpleMes.MultiTenancy.Dto;
using SC.SimpleMes.Users;
using SC.SimpleMes.Users.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    ///  个人账号管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [AbpMvcAuthorize]
    public class UserAccountController: SimpleMesControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IUserAppService _userAppService;
        private readonly TenantManager _tenantManager;
        private readonly IMapper _mapper;
        private readonly ITenantCache _tenantCache;

        /// <summary>
        /// 个人账号管理
        /// </summary>
        public UserAccountController(UserManager userManager, IUserAppService userAppService, TenantManager tenantManager, IMapper mapper, ITenantCache tenantCache)
        {
            _userManager = userManager;
            _userAppService = userAppService;
            _tenantManager = tenantManager;
            _mapper = mapper;
            _tenantCache = tenantCache;
        }


        /// <summary>
        /// 修改密码后需要退出重新登录
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Activation)]

        public async Task<JHTAjaxResponse> ChangePassWordAsync([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userInfo = await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault());
            var result = await _userManager.ChangePasswordAsync(userInfo, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return new JHTAjaxResponse() { Msg = "密码修改成功，请重新登录" };
            }
            else
            {
                return new JHTAjaxResponse() { Code = 400, Msg = string.Join(",", result.Errors.Select(p => p.Description)) };
            }
        }

        /// <summary>
        /// 修改电话号码
        /// </summary>
        /// <param name="numberDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task<JHTAjaxResponse> ChangePhoneNumberAsync([FromBody] ChangePhoneNumberDto numberDto)
        {
            var userInfo = await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault());
            if (_userManager.PasswordHasher.VerifyHashedPassword(userInfo, userInfo.Password, numberDto.PassWord) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                return new JHTAjaxResponse() { Msg = "用户密码错误" };
            }

            var result = await _userManager.ChangePhoneNumberAsync(userInfo, numberDto.NewPhone, numberDto.ValidCode);
            if (result.Succeeded)
            {
                return new JHTAjaxResponse() { Msg = "电话号码修改成功" };
            }
            else
            {
                return new JHTAjaxResponse() { Msg = string.Join(",", result.Errors.Select(p => p.Description)) };
            }
        }

        /// <summary>
        /// 根据手机号找回密码
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<JHTAjaxResponse> FindBackPassWordAsync([FromBody] FindBackPassWordDto fbcode)
        {
            var user = await _userManager.FindByPhoneNumberAsync(fbcode.PhoneNumber);
            var result = await _userManager.ResetPasswordAsync(user, fbcode.Code, fbcode.NewPassWord);
            if (result.Succeeded)
            {
                return new JHTAjaxResponse() { Msg = "密码找回成功" };
            }
            else
            {
                return new JHTAjaxResponse() { Code = 500, Msg = string.Join(",", result.Errors.Select(p => p.Description)) };
            }
        }

        /// <summary>
        /// 加载当前用户的基础信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<UserDto>> LoadUserBasicInfoAsync()
        {
            var userDto = await _userAppService.GetAsync(new EntityDto<long>(AbpSession.UserId.GetValueOrDefault()));
            return new JHTAjaxResponse<UserDto>()
            {
                Data = userDto
            };
        }

        /// <summary>
        /// 获取当前登录用户已有的权限信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<string>>> LoadUserPermissionList()
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.GetValueOrDefault());
            var permissionList = await _userManager.GetGrantedPermissionsAsync(user);
            var data = permissionList.Select(p => p.Name).ToList();
            return new JHTAjaxResponse<List<string>>(data);
        }

        /// <summary>
        /// 修改个人基础信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task<JHTAjaxResponse> UpdateAccountInfoAsync([FromBody] UpdateUserDto userInfo)
        {
            var result = await _userAppService.UpdateUserBasicInfoAsync(userInfo);
            if (result.Succeeded)
            {
                return new JHTAjaxResponse() { Msg = "基础信息修改成功" };
            }
            else
            {
                return new JHTAjaxResponse() { Msg = string.Join(",", result.Errors.Select(p => p.Description)) };
            }
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="files">文件</param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task<JHTAjaxResponse<string>> UploadHeadImgAsync([FromForm] IFormFile files)
        {
            if (files == null && Request.Form.Files.Count > 0)
            {
                files = Request.Form.Files[0];
            }

            var result = await _userAppService.SaveHeadImageAsync(files);

            return new JHTAjaxResponse<string>(result) { Msg = "图片上传成功" };
        }
    }
}

