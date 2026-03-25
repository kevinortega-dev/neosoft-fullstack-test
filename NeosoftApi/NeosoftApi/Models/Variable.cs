using System.ComponentModel.DataAnnotations;

namespace NeosoftApi.Models
{
    public class Variable
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Valor { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty;
        // valores validos: "texto", "numérico", "booleano"
    }
}