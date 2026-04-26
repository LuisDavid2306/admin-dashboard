namespace AdminDashboard.Models
{
    public class DashboardViewModel
    {
        public int TotalUsuarios { get; set; }
        public int TotalTransacciones { get; set; }
        public int TotalGrupos { get; set; }
        public decimal TotalDineroSistema { get; set; }

        public List<TransaccionesPorDia> TransaccionesPorDia { get; set; } = new();
        public List<DineroPorDia> DineroPorDia { get; set; } = new();
        public List<TopUsuario> TopUsuarios { get; set; } = new();
    }
    public class TransaccionesPorDia
    {
        public string Fecha { get; set; } = "";
        public int Cantidad { get; set; }
    }

    public class DineroPorDia
    {
        public string Fecha { get; set; } = "";
        public decimal Total { get; set; }
    }

    public class TopUsuario
    {
        public string Nombre { get; set; } = "";
        public decimal TotalMovido { get; set; }
    }
}
