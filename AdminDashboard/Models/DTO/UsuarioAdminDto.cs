namespace AdminDashboard.Models.DTO
{
    public class UsuarioAdminDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
        public decimal Saldo { get; set; }
    }
}
