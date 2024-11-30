using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Crud_Ligaut.Shared
{
    public class Liga
    {
        public int Id { get; set; } // Identificador único

        [Required]
        public string? Jugador { get; set; } // Nombre del jugador

        [Required]
        public string? NombreCompleto { get; set; } // Nombre completo del jugador

        [Required]
        public int Numero { get; set; } // Número del jugador en el equipo

        [Required]
        public string? Equipo { get; set; } // Nombre del equipo al que pertenece

        public string? Alias { get; set; } // Apodo del jugador (opcional)

        [Required]
        public int Estado { get; set; } // Estado del jugador (ej. Activo, Inactivo)
    }
}
