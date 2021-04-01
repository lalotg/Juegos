using System;

namespace Juegos.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fundacion { get; set; }
        public string Logotipo{get;set;}
    }
}