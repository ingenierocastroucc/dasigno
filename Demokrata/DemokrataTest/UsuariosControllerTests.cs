using Xunit;
using Demokrata.Models;
using Demokrata.Services;
using Demokrata.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demokrata.Data;

public class UsuariosControllerTests
{
    private readonly UsuariosController _controller;
    private readonly UsuarioService _usuarioService;
    private readonly ApplicationDbContext _context;

    public UsuariosControllerTests()
    {
        // Usamos una base de datos en memoria para pruebas
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Nombre de la base de datos en memoria
            .Options;

        // Crear el contexto de la base de datos en memoria
        _context = new ApplicationDbContext(options);

        // Crear una instancia del servicio pasando el contexto en memoria
        _usuarioService = new UsuarioService(_context);

        // Inyectamos el servicio en el controlador
        _controller = new UsuariosController(_usuarioService);
    }

    #region Tests para CrearUsuario

    [Fact]
    public async Task CrearUsuarioValidado()
    {
        // Arrange: Creacion de un usuario simulado con un sueldo válido
        var usuario = new Usuario { Id = 1, PrimerNombre = "Juan", PrimerApellido = "Perez", Sueldo = 5000 };

        // Act: Llamado al método CrearUsuario del controlador
        var result = await _controller.CrearUsuario(usuario);

        // Assert: Verificacion que la respuesta CreatedAtAction
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, actionResult.StatusCode);  
        Assert.Equal("ObtenerUsuarioPorId", actionResult.ActionName); 
        Assert.Equal(usuario.Id, actionResult.RouteValues["id"]); 
    }

    #endregion

    #region Tests para ObtenerUsuarioPorId

    [Fact]
    public async Task ObtenerUsuarioPorId()
    {
        // Arrange: Limpieza de usuarios existentes y creacion de un usuario simulado
        _context.Usuarios.RemoveRange(_context.Usuarios);
        await _context.SaveChangesAsync();

        var usuario = new Usuario { Id = 1, PrimerNombre = "Juan", PrimerApellido = "Perez" };

        // Simulacion de servicio que agrega el usuario
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act: Llamado al método ObtenerUsuarioPorId del controlador
        var result = await _controller.ObtenerUsuarioPorId(1);

        // Assert: Verificacion de la respuesta OkObjectResult
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
        var usuarioResultado = Assert.IsType<Usuario>(actionResult.Value);
        Assert.Equal(usuario.Id, usuarioResultado.Id);
    }

    #endregion

    #region Tests para ObtenerUsuarios
    #endregion

    #region Tests para ActualizarUsuario

    [Fact]
    public async Task ActualizarUsuarioExistente()
    {
        // Arrange: Limpieza de usuarios existentes en la base de datos en memoria
        _context.Usuarios.RemoveRange(_context.Usuarios);
        await _context.SaveChangesAsync();

        // Creacion de un usuario para actualizar
        var usuario = new Usuario { Id = 1, PrimerNombre = "Juan", PrimerApellido = "Perez" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Cambio de nombre
        usuario.PrimerNombre = "Juanito";

        // Act: Llamamos al método ActualizarUsuario del controlador
        var result = await _controller.ActualizarUsuario(1, usuario);

        // Assert: Verificacion de la respuesta OkObjectResult con el usuario actualizado
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
        var usuarioResultado = Assert.IsType<Usuario>(actionResult.Value);
        Assert.Equal(usuario.Id, usuarioResultado.Id);
        Assert.Equal("Juanito", usuarioResultado.PrimerNombre);
    }

    #endregion

    #region Tests para EliminarUsuario

    [Fact]
    public async Task EliminarUsuarioExistente()
    {
        // Arrange: Limpieza de usuarios existentes en la base de datos en memoria
        _context.Usuarios.RemoveRange(_context.Usuarios);
        await _context.SaveChangesAsync();

        // Creacion de usuario para eliminar
        var usuario = new Usuario { Id = 1, PrimerNombre = "Juan", PrimerApellido = "Perez" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act: Llamado al método EliminarUsuario del controlador
        var result = await _controller.EliminarUsuario(1);

        // Assert: Verificacion de la respuesta NoContentResult
        var actionResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, actionResult.StatusCode);

        // Verifica que el usuario ha sido eliminado
        var usuarioEliminado = await _context.Usuarios.FindAsync(1);
        Assert.Null(usuarioEliminado);
    }

    [Fact]
    public async Task EliminarUsuarioNoExistente()
    {
        // Act: Llamado al método EliminarUsuario con un Id que no existe
        var result = await _controller.EliminarUsuario(999);

        // Assert: Verificacion de la respuesta es NotFoundResult
        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, actionResult.StatusCode); 
    }

    #endregion

    #region Test para CrearUsuario con Modelo Inválido

    [Fact]
    public async Task CrearUsuarioInvalidModel()
    {
        // Arrange: Simulacion de modelo inválido (sin el PrimerNombre)
        _controller.ModelState.AddModelError("PrimerNombre", "Required");
        var usuario = new Usuario();

        // Act: Llamamdo al método CrearUsuario del controlador
        var result = await _controller.CrearUsuario(usuario);

        // Assert: Verificacion de la respuesta BadRequest
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, actionResult.StatusCode);
    }

    #endregion
}