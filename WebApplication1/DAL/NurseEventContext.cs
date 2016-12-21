using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.DAL
{
    public class NurseEventContext : DbContext
    {
        public NurseEventContext()
            : base("OracleDbContext")
        {
        }

        public DbSet<NurseEvent> NurseEvents { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Properties().Where(x => x.PropertyType.FullName.Equals("System.String") && !x.GetCustomAttributes(false).OfType<System.ComponentModel.DataAnnotations.Schema.ColumnAttribute>().Where(q => q.TypeName != null && q.TypeName.Equals("varchar", StringComparison.InvariantCultureIgnoreCase)).Any()).Configure(c => c.HasColumnType("varchar"));
            //modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar2").HasMaxLength(4000).IsUnicode(false));
            //modelBuilder.Properties().Where(n => n.Name == "event_cause").Configure(c => c.HasMaxLength(2000));
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"^event_.*$");
            modelBuilder.Properties<string>().Where(n => rgx.IsMatch(n.Name)).Configure(c => c.HasColumnType("varchar2").HasMaxLength(2000));
            modelBuilder.Properties<string>().Where(n => !rgx.IsMatch(n.Name)).Configure(c => c.HasColumnType("varchar2").HasMaxLength(50));
            modelBuilder.HasDefaultSchema("SYF");
        }
    }
}