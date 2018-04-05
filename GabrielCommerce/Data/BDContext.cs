using GabrielCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace GabrielCommerce.Data
{
    public class BDContext : DbContext
    {
        public BDContext(DbContextOptions<BDContext> options) : base(options)
        { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> Itens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PedidoItem>().HasKey(t => new { t.PedidoId, t.ProdutoId });
        }
    }
}
