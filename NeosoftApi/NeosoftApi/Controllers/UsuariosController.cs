using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeosoftApi.Data;
using NeosoftApi.DTOs;
using NeosoftApi.Models;

namespace NeosoftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsuariosController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios
                .Include(u => u.Rol)
                .Select(u => new UsuarioResponseDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    RolId = u.RolId,
                    NombreRol = u.Rol!.Nombre
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return Ok(new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                RolId = usuario.RolId,
                NombreRol = usuario.Rol!.Nombre
            });
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> CrearUsuario(UsuarioDto dto)
        {
            // verificar que el rol existe
            var rolExiste = await _db.Roles.AnyAsync(r => r.Id == dto.RolId);
            if (!rolExiste)
                return BadRequest(new { mensaje = "El rol seleccionado no existe" });

            // verificar email duplicado
            var emailExiste = await _db.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (emailExiste)
                return BadRequest(new { mensaje = "Ya existe un usuario con ese email" });

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                RolId = dto.RolId
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            await _db.Entry(usuario).Reference(u => u.Rol).LoadAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id },
                new UsuarioResponseDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    RolId = usuario.RolId,
                    NombreRol = usuario.Rol!.Nombre
                });
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, UsuarioDto dto)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            // verificar que el rol existe
            var rolExiste = await _db.Roles.AnyAsync(r => r.Id == dto.RolId);
            if (!rolExiste)
                return BadRequest(new { mensaje = "El rol seleccionado no existe" });

            // verificar email duplicado excluyendo el usuario actual
            var emailExiste = await _db.Usuarios
                .AnyAsync(u => u.Email == dto.Email && u.Id != id);
            if (emailExiste)
                return BadRequest(new { mensaje = "Ya existe un usuario con ese email" });

            usuario.Nombre = dto.Nombre;
            usuario.Email = dto.Email;
            usuario.RolId = dto.RolId;

            await _db.SaveChangesAsync();
            await _db.Entry(usuario).Reference(u => u.Rol).LoadAsync();

            return Ok(new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                RolId = usuario.RolId,
                NombreRol = usuario.Rol!.Nombre
            });
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario eliminado correctamente" });
        }
    }
}