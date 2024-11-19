using Demokrata.Models;
using Microsoft.AspNetCore.Mvc;
using Demokrata.Services;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [Route("CrearUsuario")]
    public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuario);
        return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = nuevoUsuario.Id }, nuevoUsuario);
    }

    [HttpGet("ObtenerUsuarioPorId/{id}")]
    public async Task<IActionResult> ObtenerUsuarioPorId(int id)
    {
        var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpGet]
    [Route("ObtenerUsuarios")]
    public async Task<IActionResult> ObtenerUsuarios([FromQuery] string? primerNombre, [FromQuery] string? primerApellido, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var usuarios = await _usuarioService.ObtenerUsuariosAsync(primerNombre, primerApellido, pageNumber, pageSize);
        return Ok(usuarios);
    }

    [HttpPut("ActualizarUsuario/{id}")]
    public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuarioActualizado = await _usuarioService.ActualizarUsuarioAsync(id, usuario);
        if (usuarioActualizado == null)
            return NotFound();

        return Ok(usuarioActualizado);
    }

    [HttpDelete("EliminarUsuario/{id}")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var eliminado = await _usuarioService.EliminarUsuarioAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}