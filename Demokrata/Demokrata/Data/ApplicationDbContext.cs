namespace Demokrata.Data
{
    using Demokrata.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        // Ejecucion de semillas con data
        public static void Seed(ApplicationDbContext context)
        {
            // Verificar si ya existen usuarios
            if (!context.Usuarios.Any()) // Si no hay usuarios, se agregan
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario
                    {
                        PrimerNombre = "Carlos", SegundoNombre = "Alberto", PrimerApellido = "Gonzalez",
                        SegundoApellido = "Martinez", FechaNacimiento = new DateTime(1990, 5, 15),
                        Sueldo = 3500.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Laura", SegundoNombre = "Beatriz", PrimerApellido = "Perez",
                        SegundoApellido = "Lopez", FechaNacimiento = new DateTime(1985, 11, 2),
                        Sueldo = 4000.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Juan", SegundoNombre = "Carlos", PrimerApellido = "Sanchez",
                        SegundoApellido = "Ramirez", FechaNacimiento = new DateTime(1993, 8, 25),
                        Sueldo = 3000.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Maria", SegundoNombre = "Cristina", PrimerApellido = "Moreno",
                        SegundoApellido = "Fernandez", FechaNacimiento = new DateTime(1980, 3, 12),
                        Sueldo = 5000.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "David", SegundoNombre = "Antonio", PrimerApellido = "Lopez",
                        SegundoApellido = "Gomez", FechaNacimiento = new DateTime(1995, 7, 19),
                        Sueldo = 2800.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Patricia", SegundoNombre = "Delia", PrimerApellido = "Diaz",
                        SegundoApellido = "Rodriguez", FechaNacimiento = new DateTime(1992, 9, 3),
                        Sueldo = 4200.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Pedro", SegundoNombre = "Luis", PrimerApellido = "Ruiz",
                        SegundoApellido = "Hernandez", FechaNacimiento = new DateTime(1991, 6, 30),
                        Sueldo = 3900.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Ana", SegundoNombre = "Maria", PrimerApellido = "Martinez",
                        SegundoApellido = "Lopez", FechaNacimiento = new DateTime(1988, 2, 18),
                        Sueldo = 4600.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Jorge", SegundoNombre = "Manuel", PrimerApellido = "Serrano",
                        SegundoApellido = "Jimenez", FechaNacimiento = new DateTime(1994, 4, 5),
                        Sueldo = 3100.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    },
                    new Usuario
                    {
                        PrimerNombre = "Isabel", SegundoNombre = "Teresa", PrimerApellido = "Alvarez",
                        SegundoApellido = "Castro", FechaNacimiento = new DateTime(1990, 1, 22),
                        Sueldo = 4700.00m, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                    }
                };

                // Solo se insertan usuarios si no hay usuarios con el mismo nombre
                foreach (var usuario in usuarios)
                {
                    // Comprobamos si el usuario ya existe por nombre completo
                    if (!context.Usuarios.Any(u =>
                        u.PrimerNombre == usuario.PrimerNombre &&
                        u.PrimerApellido == usuario.PrimerApellido &&
                        u.FechaNacimiento == usuario.FechaNacimiento))
                    {
                        context.Usuarios.Add(usuario);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}