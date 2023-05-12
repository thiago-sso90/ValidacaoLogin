using Microsoft.EntityFrameworkCore;
using ValidacaoLogin.Entidades;

namespace ValidacaoLogin.Persistencia
{ 

    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> contex) : base(contex)
        {
        
        }
    
        public DbSet <Usuario> usuarios { get; set; }
    
    }

    
}
