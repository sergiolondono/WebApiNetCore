using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPrueba.Entities
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener máximo {1} caracteres")]
        public string Nombre { get; set; }
        [Range(18, 120)]
        public int Edad { get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Nombre)});
                }
            }
        }
    }
}
