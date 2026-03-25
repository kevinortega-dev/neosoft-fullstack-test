using System.ComponentModel.DataAnnotations;

namespace NeosoftApi.DTOs
{
    public class VariableDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor es obligatorio")]
        public string Valor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [RegularExpression("texto|numérico|booleano", 
            ErrorMessage = "El tipo debe ser: texto, numérico o booleano")]
        public string Tipo { get; set; } = string.Empty;
    }

    public class VariableResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}