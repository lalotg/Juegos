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
    public class EquiposController : ControllerBase
    {
        private readonly JuegoContext _context;

        public EquiposController(JuegoContext context){
            _context = context;
        }

        //GET: api/equipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetMenus(){
            return await _context.Equipos
                .ToArrayAsync();
        }

        //GET: api/equipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> GetEquipo(int id){
            var equipo = await _context.Equipos.FindAsync(id);
            
            if(equipo == null)
            {
                return NotFound();
            }

            return equipo;
        }

        //PUT: api/equipos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id,Equipo equipo)
        {
            if(id!=equipo.EquipoId){
                return BadRequest();
            }

            _context.Entry(equipo).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!EquipoExists(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/equipos
        [HttpPost]
        public async Task<ActionResult<Equipo>> PostEquipo(Equipo equipo){
            
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipo),new{id= equipo.EquipoId},equipo);
        }

        private bool EquipoExists(int id){
            return _context.Equipos.Any(e=>e.EquipoId == id);
        }
    }
}