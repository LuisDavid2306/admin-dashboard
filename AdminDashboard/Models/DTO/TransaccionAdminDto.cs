namespace AdminDashboard.Models.DTO
{
    public class TransaccionAdminDto
    {
        public string CodTransaccion { get; set; } = "";
        public string Tipo { get; set; } = "";
        public decimal Monto { get; set; }
        public string UsuarioOrigen { get; set; } = "";
        public string UsuarioDestino { get; set; } = "";
        public DateTime Fecha { get; set; }
    }
}
