using System;

namespace Juegos.Models
{
    public class Partido
    {
        public int PartidoId { get; set; }
        public DateTime Fecha { get; set; }
        public int Anfitrion { get; set; }
        public int Visitante { get; set; }
        public string Marcador { get; set; }
        
        
    }
}