using Abp.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Roles;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.MultiTenancy;

namespace SC.SimpleMes.EntityFrameworkCore
{
    public partial class K3ERPDbContext : AbpDbContext
    {
        public K3ERPDbContext(DbContextOptions<K3ERPDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<K3MaterialInfo> k3MaterialInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<K3MaterialInfo>(p =>
            {
                p.ToTable("t_Item");
                p.Property(p => p.Id).HasColumnName("FItemID");
            });
        }
    }
}
