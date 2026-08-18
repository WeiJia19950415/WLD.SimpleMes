using FluentExcel;
using System.ComponentModel;

namespace SC.SimpleMes.Models
{
    public class UserExcelModel
    {

        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("电子邮箱")]
        public string EmailAddress { get; set; }
        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }
        [DisplayName("职务")]
        public string Postion { get; set; }
        [DisplayName("工作地址")]
        public string WorkAddress { get; set; }

        /// <summary>
        /// 导入导出excel配置
        /// </summary>
        public static void FluentConfiguration()
        {
            var fc = Excel.Setting.For<UserExcelModel>();
            fc.Property(r => r.Name)
              .HasExcelIndex(0)
              .HasExcelTitle("姓名");
            fc.Property(r => r.EmailAddress)
              .HasExcelIndex(1)
              .HasExcelTitle("电子邮箱");
            fc.Property(r => r.PhoneNumber)
              .HasExcelIndex(2)
              .HasExcelTitle("手机号码");
            fc.Property(r => r.Postion)
              .HasExcelIndex(3)
              .HasExcelTitle("职务");
            fc.Property(r => r.WorkAddress)
              .HasExcelIndex(4)
              .HasExcelTitle("工作地址");
        }
    }
}

