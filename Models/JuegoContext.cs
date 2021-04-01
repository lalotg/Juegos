using Microsoft.EntityFrameworkCore;

namespace Juegos.Models
{
    public class JuegoContext : DbContext
    {
        public JuegoContext(DbContextOptions<JuegoContext> options)
            : base(options)
        {
        }

        public DbSet<Equipo> Equipos {get; set;}
        public DbSet<Jugador> Jugadores { get; set; }
        
        
    }
}