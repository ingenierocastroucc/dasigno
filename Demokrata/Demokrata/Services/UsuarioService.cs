using Demokrata.Data;
using Demokrata.Models;
using Microsoft.EntityFrameworkCore;

namespace Demokrata.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            if (usuario.Sueldo <= 0)
                throw new ArgumentException("El sueldo no puede ser 0 o menor.");

            usuario.FechaCreacion = DateTime.UtcNow;
            usuario.FechaModificacion = DateTime.UtcNow;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync(string? primerNombre, string? primerApellido, int pageNumber, int pageSize)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(primerNombre))
                query = query.Where(u => u.PrimerNombre.Contains(primerNombre));

            if (!string.IsNullOrEmpty(primerApellido))
                query = query.Where(u => u.PrimerApellido.Contains(primerApellido));

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Usuario> ActualizarUsuarioAsync(int id, Usuario usuarioActualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return null;

            usuario.PrimerNombre = usuarioActualizado.PrimerNombre;
            usuario.SegundoNombre = usuarioActualizado.SegundoNombre;
            usuario.PrimerApellido = usuarioActualizado.PrimerApellido;
            usuario.SegundoApellido = usuarioActualizado.SegundoApellido;
            usuario.FechaNacimiento = usuarioActualizado.FechaNacimiento;
            usuario.Sueldo = usuarioActualizado.Sueldo;
            usuario.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}