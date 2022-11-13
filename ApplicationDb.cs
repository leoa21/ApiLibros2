using ApiLibros2.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros2
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LibroAutor>()
                .HasKey(li => new { li.LibroId, li.AutorId });
        }

        public DbSet<Libro> Libros { get; set; }

        public DbSet<Autor> Autor { get; set; }

        public DbSet<Editorial> Editorial { get; set; }

        public DbSet<LibroAutor> LibroAutor { get; set; }

    }
}
