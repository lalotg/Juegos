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
    public class JugadoresController : ControllerBase
    {
        private readonly JuegoContext _context;
        
        public JugadoresController(JuegoContext context){
            _context = context;
        }

        //GET: api/jugadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetJugadores(){
            return await _context.Jugadores
                .ToArrayAsync();
        }

        //GET: api/jugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugador>> GetEquipo(int id){
            var jugador = await _context.Jugadores.FindAsync(id);
            
            if(jugador == null)
            {
                return NotFound();
            }

            return jugador;
        }

        //PUT: api/jugadores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id,Jugador jugador)
        {
            if(id!=jugador.JugadorId){
                return BadRequest();
            }

            _context.Entry(jugador).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!JugadorExists(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/jugadores
        [HttpPost]
        public async Task<ActionResult<Jugador>> PostEquipo(Jugador jugador){
            
            _context.Jugadores.Add(jugador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugadores),new{id= jugador.JugadorId},jugador);
        }


        private bool JugadorExists(int id){
            return _context.Jugadores.Any(e=>e.JugadorId == id);
        }
    }
}