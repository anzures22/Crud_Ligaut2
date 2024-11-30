using Crud_Ligaut.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Ligaut.Server.Controllers
{
    [ApiController]
    [Route("api/ligas")]
    public class LigaController : ControllerBase
    {
        private readonly SQLDBContext _context;

        public LigaController(SQLDBContext context)
        {
            _context = context;
        }

        // Método para obtener todas las ligas (filtradas por estado)
        [HttpGet("getligas")]
        public async Task<IActionResult> GetLigasAsync()
        {
            try
            {
                var list = await _context.Liga
                    .Where(p => p.Estado == 1)
                    .ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos: {ex.Message}");
            }
        }

        // Método para obtener una liga específica por su ID
        [HttpGet("getliga/{id:int}")]
        public async Task<IActionResult> GetLigaAsync(int id)
        {
            try
            {
                var liga = await _context.Liga
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (liga == null)
                {
                    return NotFound($"No se encontró una liga con el ID {id}.");
                }

                return Ok(liga);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los datos: {ex.Message}");
            }
        }

        // Método para agregar una liga
        [HttpPost("addligas")]
        public async Task<IActionResult> AddLiga(Liga liga)
        {
            try
            {
                if (liga != null)
                {
                    _context.Add(liga);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return BadRequest("Datos inválidos");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar la liga: {ex.Message}");
            }
        }

        // Método para editar una liga (usando PUT)
        [HttpPut("editligas/{id}")]
        public async Task<IActionResult> EditLiga(int id, Liga liga)
        {
            if (liga == null || liga.Id != id)
            {
                return BadRequest("Datos inválidos o el ID no coincide.");
            }

            _context.Entry(liga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(liga.Id))
                {
                    return NotFound($"No se encontró una liga con el ID {liga.Id}.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar la liga: {ex.Message}");
            }
        }

        // Método para eliminar una liga
        [HttpDelete("deleteligas/{id}")]
        public async Task<IActionResult> DeleteLiga(int id)
        {
            try
            {
                var liga = await _context.Liga.FindAsync(id);
                if (liga == null)
                {
                    return NotFound($"No se encontró una liga con el ID {id}.");
                }

                _context.Liga.Remove(liga);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la liga: {ex.Message}");
            }
        }

        private bool Exists(int id)
        {
            return (_context.Liga?.Any(p => p.Id == id)).GetValueOrDefault();
        }
    }
}
