using ApiStore.DB.Mapping;
using ApiStore.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.DB
{
    public class StoreContext : DbContext
    {
        public virtual DbSet<Cliente> Clientes { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteMap());
        }
    }
}
