using Microsoft.EntityFrameworkCore;

namespace Comandas.Api;

public class ComandasDbContext : DbContext
{
    public ComandasDbContext(
        DbContextOptions<ComandasDbContext> options
    ) : base(options)
    { }

    // definir algumas configuracoes adicionais no banco
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Usuario>()
            .HasData(
                new Models.Usuario
                {
                    Id = 1,
                    Nome = "Admin",
                    Email = "admin@admin.com",
                    Senha = "admin123"
                }
             );
        base.OnModelCreating(modelBuilder);

    }
    public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
    public DbSet<Models.Mesa> Mesas { get; set; } = default!;
    public DbSet<Models.Reserva> Reservas { get; set; } = default!;
    public DbSet<Models.Comanda> Comandas { get; set; } = default!;
    public DbSet<Models.ComandaItem> ComandaItens { get; set; } = default!;
    public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
    public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItens { get; set; } = default!;
    public DbSet<Models.CardapioItem> CardapioItems { get; set; } = default!;
}
