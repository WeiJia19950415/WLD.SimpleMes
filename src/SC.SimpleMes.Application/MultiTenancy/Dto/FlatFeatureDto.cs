using Abp.Application.Features;
using Abp.AutoMapper;

namespace SC.SimpleMes.MultiTenancy.Dto
{
    [AutoMap(typeof(Feature))]
    public class FlatFeatureDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Vlaue { get; set; }

        public int TenandId { get; set; }
    }
}

