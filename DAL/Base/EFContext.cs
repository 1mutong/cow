using DairyCow.Model;
using System.Data.Entity;

namespace DairyCow.DAL.Base
{
    public class EFContext : DbContext
    {
        public DbSet<Cow> Cows { get; set; }
        public DbSet<Insemination> Inseminations { get; set; }

        public EFContext()
            : base("DairyCowConnection")
        {            
        }
    }
}
