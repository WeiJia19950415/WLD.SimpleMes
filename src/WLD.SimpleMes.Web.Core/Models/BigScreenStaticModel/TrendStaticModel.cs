using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Models.BigScreenStaticModel
{
    public class TrendStaticModel<T>
    {
        public TrendStaticModel()
        {
            XDataInfo = new List<string>();
            YDataInfo = new List<List<T>>()
            {
            };
        }

        public List<string> XDataInfo { get; set; }

        public List<List<T>> YDataInfo { get; set; }
    }
}
