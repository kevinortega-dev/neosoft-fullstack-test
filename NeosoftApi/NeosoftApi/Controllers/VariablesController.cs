using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeosoftApi.Data;
using NeosoftApi.DTOs;
using NeosoftApi.Models;

namespace NeosoftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariablesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VariablesController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/variables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VariableResponseDto>>> GetVariables()
        {
            var variables = await _db.Variables
                .Select(v => new VariableResponseDto
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Valor = v.Valor,
                    Tipo = v.Tipo
                })
                .ToListAsync();

            return Ok(variables);
        }

        // GET: api/variables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariableResponseDto>> GetVariable(int id)
        {
            var variable = await _db.Variables.FindAsync(id);

            if (variable == null)
                return NotFound(new { mensaje = "Variable no encontrada" });

            return Ok(new VariableResponseDto
            {
                Id = variable.Id,
                Nombre = variable.Nombre,
                Valor = variable.Valor,
                Tipo = variable.Tipo
            });
        }

        // POST: api/variables
        [HttpPost]
        public async Task<ActionResult<VariableResponseDto>> CrearVariable(VariableDto dto)
        {
            var tiposValidos = new[] { "texto", "numérico", "booleano" };
            if (!tiposValidos.Contains(dto.Tipo))
                return BadRequest(new { mensaje = "Tipo inválido. Use: texto, numérico o booleano" });

            var variable = new Variable
            {
                Nombre = dto.Nombre,
                Valor = dto.Valor,
                Tipo = dto.Tipo
            };

            _db.Variables.Add(variable);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVariable), new { id = variable.Id },
                new VariableResponseDto
                {
                    Id = variable.Id,
                    Nombre = variable.Nombre,
                    Valor = variable.Valor,
                    Tipo = variable.Tipo
                });
        }

        // PUT: api/variables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVariable(int id, VariableDto dto)
        {
            var variable = await _db.Variables.FindAsync(id);

            if (variable == null)
                return NotFound(new { mensaje = "Variable no encontrada" });

            var tiposValidos = new[] { "texto", "numérico", "booleano" };
            if (!tiposValidos.Contains(dto.Tipo))
                return BadRequest(new { mensaje = "Tipo inválido. Use: texto, numérico o booleano" });

            variable.Nombre = dto.Nombre;
            variable.Valor = dto.Valor;
            variable.Tipo = dto.Tipo;

            await _db.SaveChangesAsync();

            return Ok(new VariableResponseDto
            {
                Id = variable.Id,
                Nombre = variable.Nombre,
                Valor = variable.Valor,
                Tipo = variable.Tipo
            });
        }

        // DELETE: api/variables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVariable(int id)
        {
            var variable = await _db.Variables.FindAsync(id);

            if (variable == null)
                return NotFound(new { mensaje = "Variable no encontrada" });

            _db.Variables.Remove(variable);
            await _db.SaveChangesAsync();

            return Ok(new { mensaje = "Variable eliminada correctamente" });
        }
    }
}