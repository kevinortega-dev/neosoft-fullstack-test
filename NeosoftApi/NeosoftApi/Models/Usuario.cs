using System.ComponentModel.DataAnnotations;

namespace NeosoftApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public int RolId { get; set; }

        // navegacion hacia Rol
        public Rol? Rol { get; set; }
    }
}