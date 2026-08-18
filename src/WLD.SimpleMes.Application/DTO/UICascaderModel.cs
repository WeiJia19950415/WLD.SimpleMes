using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DTO
{
    public class UICascaderModel<TData, TValue> where TData : class
    {
        public string Label { get; set; }
        public TValue Value { get; set; }
        public bool Leaf { get; set; }

        /// <summary>
        /// 子项信息
        /// </summary>
        public List<UICascaderModel<TData, TValue>> Children { get; set; }

        public TData Data { get; set; }
    }
}
