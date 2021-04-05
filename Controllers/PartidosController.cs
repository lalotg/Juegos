using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Juegos.Models;

namespace Juegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly JuegoContext _context;
        public PartidosController(JuegoContext context){
            _context = context;
        }

        //GET: api/partidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidos(){
            return await _context.Partidos
                .ToArrayAsync();
        }

        //GET: api/partidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partido>> GetEquipo(int id){
            var partido = await _context.Partidos.FindAsync(id);
            
            if(partido == null)
            {
                return NotFound();
            }

            return partido;
        }

        //PUT: api/partidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id,Partido partido)
        {
            if(id!=partido.PartidoId){
                return BadRequest();
            }

            _context.Entry(partido).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!PartidoExist(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/partidos
        [HttpPost]
        public async Task<ActionResult<Partido>> PostEquipo(Partido partido){
            
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartidos),new{id= partido.PartidoId},partido);
        }

        private bool PartidoExist(int id){
            return _context.Partidos.Any(e=>e.PartidoId == id);
        }
    }
}