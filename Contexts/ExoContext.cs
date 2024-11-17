using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext()
        {
        }
        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
            {
                if(!OptionsBuilder.IsConfigured)
                {
                    // Essa string de conexão depende da sua máquina.
                    OptionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS;" +

                    "Database=ExoApi;Trusted_connection=True;");
                
                  // Exemplo 1 de string de conexão:
                  //User ID=sa;Password=Gomes0906;Server=localhost;Database=ExoApi;
                  //trusted_Connection=False;

                 // Exemplo 2 de string de conexão:
                 // Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;
                }
            }

            public DbSet<Projeto> Projetos {get; set; }

            public DbSet<Usuario> Usuarios {get; set; }
    }
 
}