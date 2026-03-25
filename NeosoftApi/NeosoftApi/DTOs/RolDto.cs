using System.ComponentModel.DataAnnotations;

namespace NeosoftApi.DTOs
{
    public class RolDto
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
    }

    public class RolResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}