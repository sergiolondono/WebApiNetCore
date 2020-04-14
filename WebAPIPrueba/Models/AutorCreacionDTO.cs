using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPrueba.Models
{
    public class AutorCreacionDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener máximo {1} caracteres")]
        public string Nombre { get; set; }
        [Range(18, 120)]
        public int Edad { get; set; }
    }
}
