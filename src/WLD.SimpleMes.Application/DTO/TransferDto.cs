using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DTO
{
    /// <summary>
    /// Vue Transfer组件数据模型
    /// </summary>
    public class TransferDto
    {
        /// <summary>
        /// 绑定对象的Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 所有可绑定的对象
        /// </summary>
        public List<TransferItemDto> allList { get; set; }


        /// <summary>
        /// 已绑定的对象
        /// </summary>
        public List<long> selectList { get; set; }
    }


    public class TransferItemDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Key { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 是否显示，默认显示
        /// </summary>
        public bool disabled { get; set; } = false;
    }
}
