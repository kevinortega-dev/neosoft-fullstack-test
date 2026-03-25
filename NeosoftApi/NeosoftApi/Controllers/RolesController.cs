using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeosoftApi.Data;
using NeosoftApi.DTOs;
using NeosoftApi.Models;

namespace NeosoftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RolesController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolResponseDto>>> GetRoles()
        {
            var roles = await _db.Roles
                .Select(r => new RolResponseDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolResponseDto>> GetRol(int id)
        {
            var rol = await _db.Roles.FindAsync(id);

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            return Ok(new RolResponseDto { Id = rol.Id, Nombre = rol.Nombre });
        }

        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<RolResponseDto>> CrearRol(RolDto dto)
        {
            var rol = new Rol { Nombre = dto.Nombre };
            _db.Roles.Add(rol);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRol), new { id = rol.Id },
                new RolResponseDto { Id = rol.Id, Nombre = rol.Nombre });
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, RolDto dto)
        {
            var rol = await _db.Roles.FindAsync(id);

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            rol.Nombre = dto.Nombre;
            await _db.SaveChangesAsync();

            return Ok(new RolResponseDto { Id = rol.Id, Nombre = rol.Nombre });
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            var rol = await _db.Roles.FindAsync(id);

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            // verificar que no tenga usuarios asociados
            var tieneUsuarios = await _db.Usuarios.AnyAsync(u => u.RolId == id);
            if (tieneUsuarios)
                return BadRequest(new { mensaje = "No se puede eliminar un rol que tiene usuarios asignados" });

            _db.Roles.Remove(rol);
            await _db.SaveChangesAsync();

            return Ok(new { mensaje = "Rol eliminado correctamente" });
        }
    }
}