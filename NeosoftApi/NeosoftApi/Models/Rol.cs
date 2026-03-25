namespace NeosoftApi.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // relacion inversa
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}