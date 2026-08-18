using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using SC.SimpleMes.AttachFile;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.Models;
using SC.SimpleMes.Users;
using SC.SimpleMes.Users.Dto;
using FluentExcel;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SC.SimpleMes.Controllers
{

    [Route("api/[controller]/[action]")]
    public class UserController : SimpleMesControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IUserAppService _userAppService;
        private readonly FileSaveOptions _fileSaveOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userAppService"></param>
        /// <param name="fileSaveOptions"></param>
        public UserController(UserManager userManager
            , IUserAppService userAppService
            , IOptionsMonitor<FileSaveOptions> fileSaveOptions)
        {
            _userManager = userManager;
            _userAppService = userAppService;
            _fileSaveOptions = fileSaveOptions.CurrentValue;
        }


        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users)]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<UserDto>>> SearchUser([FromBody] JHTPageAjaxResquest<UserConditionDto> where)
        {
            return new JHTPageAjaxRespone<PageData<UserDto>>()
            {
                Data = await _userAppService.SearchUser(where)
            };
        }

        /// <summary>
        /// 批量启用禁用用户
        /// </summary>
        /// <param name="enableUserIds"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Activation)]
        public JHTAjaxResponse BatchToggleUserActiveState([FromBody] JObject enableUserIds)
        {

            var ids = enableUserIds.GetValue("ids").ToArray().Select(p => p.Value<long>()).ToArray();
            _userAppService.BatchToggleUserActiveState(ids);
            return new JHTAjaxResponse();
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users)]
        public async Task<JHTAjaxResponse> CreateUser([FromBody] UserDto dto)
        {
            var data = await _userAppService.AddUserAsync(dto);
            return new JHTAjaxResponse()
            {
                Msg = data.Data ? string.Empty : data.Message
            };
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users_ResetPassWord)]
        public async Task<JHTAjaxResponse> RestUserPassWordAsync([FromBody] ResetPasswordDto resetPasswordDto)
        {
            return await _userAppService.ResetPassWord(resetPasswordDto);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="dto">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<UserDto>> FindUser([FromBody] EntityDto<long> dto)
        {
            UserDto result = await _userAppService.GetAsync(dto);
            if (result != null)
            {
                result.RoleNames.ToList().ForEach(p =>
                {
                    p = p.ToLower();
                });
            }

            
            result.WorkStationUserRelationIds = _userAppService.GetWorkStationUserRelationIds(dto);
            return new JHTAjaxResponse<UserDto>() { Data = result };
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users)]
        public async Task<JHTAjaxResponse<IdentityResult>> UpdateUser([FromBody] UserDto dto)
        {
            ResultDto<bool> resultDto = await _userAppService.UpdateUser(dto);
            if (!resultDto.Success)
            {
                return new JHTAjaxResponse<IdentityResult>()
                {
                    Code = 500,
                    Msg = resultDto.Message
                };
            }
            return new JHTAjaxResponse<IdentityResult>()
            {
                Msg = "更新成功"
            };
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Users)]
        public async Task<JHTAjaxResponse> DeleteUser([FromBody] EntityDto<long> dto)
        {
            await _userAppService.DeleteAsync(dto);
            return new JHTAjaxResponse();
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>MO
        private bool IsPhoneVail(string phoneNumber)
        {
            Regex phoneReg = new Regex(@"^1\d{10}$");
            return phoneReg.IsMatch(phoneNumber);
        }

        /// <summary>
        /// 模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpAllowAnonymous]
        public ActionResult LoadTemplate()
        {
            string sWebRootFolder = Directory.GetCurrentDirectory();
            string sFileName = $@"wwwroot/template/人员信息导入模板.xlsx";
            var path = Path.Combine(sWebRootFolder, sFileName);
            FileStreamResult fileResult = File(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), "application/octet-stream", $"用户信息导入模板{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            return fileResult;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JHTAjaxResponse> FileIn(IFormFile file)
        {

            string sFileName = $"temp/temp_{Guid.NewGuid()}.xlsx";
            string tempPath = Path.Combine(_fileSaveOptions.DeafaultSavePath, sFileName);
            var dic = Path.GetDirectoryName(tempPath);
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            FileInfo fileInfo = new FileInfo(tempPath);
            List<string> errors = new List<string>();
            int inserts = 0, updates = 0;
            //把excelfile中的数据复制到file中
            using (FileStream fs = new FileStream(fileInfo.ToString(), FileMode.Create)) //初始化一个指定路径和创建模式的FileStream
            {
                file.CopyTo(fs);
                fs.Flush();  //清空stream的缓存，并且把缓存中的数据输出到file
            }

            var items = Excel.Load<UserExcelModel>(tempPath).ToList();

            UserImportResult importResult = new UserImportResult();
            importResult.SuccessNumber = items.Count;
            foreach (UserExcelModel item in items)
            {
                try
                {
                    if (string.IsNullOrEmpty(item.PhoneNumber))
                    {
                        importResult.FailNumber++;
                        importResult.SuccessNumber--;
                        importResult.Hint += item.Name + ":电话号码需要填写!";
                        continue;
                    }
                    if (!IsPhoneVail(item.PhoneNumber))
                    {
                        importResult.FailNumber++;
                        importResult.SuccessNumber--;
                        importResult.Hint += item.Name + ":联系号码格式不对!";
                        continue;
                    }
                    var addOrUpdate = await _userAppService.FindPhoneNumberAsync(AbpSession.TenantId, item.PhoneNumber);
                    if (addOrUpdate == null)
                    {
                        addOrUpdate = new UserDto();
                        addOrUpdate.UserName = item.PhoneNumber;
                    }
                    Random rd = new Random();
                    addOrUpdate.Name = item.Name;
                    addOrUpdate.EmailAddress = string.IsNullOrEmpty(item.EmailAddress) ? rd.Next() + "@JHTWL.COM" : item.EmailAddress;
                    addOrUpdate.PhoneNumber = item.PhoneNumber;
                    addOrUpdate.Postion = item.Postion;
                    addOrUpdate.WorkAddress = item.WorkAddress;

                    if (addOrUpdate.Id > 0)
                    {
                        await _userAppService.UpdateAndResumeAsync(addOrUpdate);
                        updates++;
                    }
                    else
                    {

                        //如果该defautl租户已经有了一个同名或同email的账户，则会导入失败
                        var defaultUserDto = await _userAppService.FindNameOrEmailAddressAsync(AbpSession.TenantId, addOrUpdate.UserName, item.EmailAddress);
                        if (defaultUserDto != null)
                        {
                            importResult.FailNumber++;
                            importResult.SuccessNumber--;
                            importResult.Hint += item.Name + ":默认租户已经存在同名或同一个电子邮箱!";
                            continue;
                        }

                        ResultDto<bool> result = await _userAppService.AddUserAsync(addOrUpdate);
                        if (!result.Success)
                        {
                            errors.Add(addOrUpdate.UserName + ":" + result.Message);
                        }
                        else
                        {
                            inserts++;
                        }
                    }

                }
                catch (Exception ex)
                {
                    importResult.FailNumber++;
                    importResult.SuccessNumber--;
                    importResult.Hint += item.Name + ex.Message;
                }
            }
            fileInfo.Delete();
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse() { };
            ajaxResponse.Data = importResult;
            return ajaxResponse;

        }

        /// <summary>
        /// 文件导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpMvcAuthorize(PermissionNames.Pages_Users)]
        public async Task<ActionResult> FileOut()
        {
            string sFileName = $@"temp/temp_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            string tempPath = Path.Combine(_fileSaveOptions.DeafaultSavePath, sFileName);
            VerifyTemp();
            PagedResultDto<UserDto> pageResult = await _userAppService.GetAllAsync(new PagedUserResultRequestDto() { MaxResultCount = int.MaxValue });
            //构建数据
            List<UserExcelModel> models = new List<UserExcelModel>();
            pageResult.Items.ToList().ForEach(item =>
            {
                UserExcelModel model = new UserExcelModel()
                {
                    Name = item.Name,
                    EmailAddress = item.EmailAddress,
                    PhoneNumber = item.PhoneNumber,
                    WorkAddress = item.WorkAddress,
                    Postion = item.Postion,
                };
                models.Add(model);
            });
            UserExcelModel.FluentConfiguration();
            models.ToExcel(tempPath);
            FileStreamResult fileResult = File(new FileStream(tempPath, FileMode.Open), "application/octet-stream", $"人员信息{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            _ = Task.Run(() => FileDelete(tempPath));//异步删除文件的运行线程
            return fileResult;

        }

        /// <summary>
        /// 创建导出文件
        /// </summary>
        private void VerifyTemp()
        {
            string sFileName = $@"temp";
            string tempPath = Path.Combine(_fileSaveOptions.DeafaultSavePath, sFileName);

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
        }

        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="path"></param>
        [AbpAuthorize]
        private void FileDelete(string path)
        {
            Thread.Sleep(5000);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                file.Delete();
        }

    }
}

