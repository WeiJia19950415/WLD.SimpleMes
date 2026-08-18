using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Models.TokenAuth
{
    public class GlobalDataDto
    {
        public SiteInfo SiteInfo { get; set; }

        public object Options { get; set; }
    }

    public class SiteInfo
    {
        public string Logo { get; set; }

        public string Title { get; set; }

        public string BgImg { get; set; }
    }
}

