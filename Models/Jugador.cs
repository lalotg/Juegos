using System;

namespace Juegos.Models
{
    public class Jugador
    {
        public int JugadorId { get; set; }
        public string Nombre{get;set;}
        public DateTime Nacimiento { get; set; }
        public string Descripcion { get; set; }
        public int EquipoId { get; set; }
        
    }
}