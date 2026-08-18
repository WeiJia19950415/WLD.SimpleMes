using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using WLD.SimpleMes.Authorization.Users;

namespace WLD.SimpleMes.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        /// <summary>
        /// �û��ʺ�
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// �û���ʵ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// �����ַ
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ����¼ʱ��
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// ���֤��
        /// </summary>
        [MaxLength(18)]
        public string IdCard { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string WorkNumber { get; set; }

        /// <summary>
        /// ְ��
        /// </summary>
        public string Postion { get; set; }

        /// <summary>
        /// ��ؽ�ɫ
        /// </summary>
        public string[] RoleNames { get; set; }

        /// <summary>
        /// ��ز���
        /// </summary>
        public string[] OrgName { get; set; }

        /// <summary>
        /// ��֯����Id
        /// </summary>
        public long[] OrgId { get; set; }
        /// <summary>
        /// �绰����
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// �⻧ID
        /// </summary>
        public long? TenantId { get; set; }
        /// <summary>
        /// ͷ���ַ
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// ���õ绰
        /// </summary>
        public string StandbyPhoneNumber { get; set; }
        /// <summary>
        /// ���õ绰��֤
        /// </summary>
        public bool IsStandbyPhoneNumberConfirmed { get; set; } = false;

        /// <summary>
        /// �绰�����Ƿ񼤻�
        /// </summary>
        public bool IsPhoneNumberConfirmed { get; set; } = false;
        /// <summary>
        /// �Ա�
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// ������ַ
        /// </summary>
        public string WorkAddress { get; set; }
        /// <summary>
        /// ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// ����ǰ��ͷ����ʾ
        /// </summary>
        public string Avatar { get { return this.HeadImage; } }
        /// <summary>
        /// �Ƿ���Ϣ����
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// ��������UserCode
        /// </summary>
        public string HKUserCode { get; set; }

        /// <summary>
        /// 所属工位ID
        /// </summary>
        public List<List<long>> WorkStationUserRelationIds { get; set; }
    }
}

