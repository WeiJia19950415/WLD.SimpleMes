using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace WLD.SimpleMes.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }
        public string AreaCode { get; set; }
        /// <summary>
        /// ͳһ������ô���
        /// </summary>
        [MaxLength(200)]
        public string UniformSocialCreditCode { get; set; }
        /// <summary>
        /// Logo��ַ
        /// </summary>
        [MaxLength(200)]
        public string LogoImage { get; set; }
        /// <summary>
        /// ��ϵ������
        /// </summary>
        [MaxLength(200)]
        [Required]

        public string ContactName { get; set; }
        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        [MaxLength(50)]
        [Required]

        public string ContactPhone { get; set; }
        /// <summary>
        /// ��ϵ������
        /// </summary>
        [MaxLength(200)]
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string ContactEmail { get; set; }
        /// <summary>
        /// ��˾��ַ
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [MaxLength(200)]
        public string OfficialWebsite { get; set; }
        /// <summary>
        /// ��˾���
        /// </summary>
        [MaxLength(2000)]
        public string BriefIntroduction { get; set; }
        /// <summary>
        /// ��˾��ģ
        /// </summary>
        public TenantScaleEnum? TenantScale { get; set; }
    }
}

