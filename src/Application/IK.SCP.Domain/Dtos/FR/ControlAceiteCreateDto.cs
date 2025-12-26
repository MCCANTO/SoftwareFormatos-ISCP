namespace IK.SCP.Domain.Dtos
{
    public class ControlAceiteCreateDto
    {
        public int LineaId { get; set; }
        public string? OrdenId { get; set; }
        public string? Producto { get; set; }
        public string? SaborId { get; set; }
        public string? OtroSabor { get; set; }
        public string Etapa { get; set; }
        public string Aceite { get; set; }
        public string InicioFuente { get; set; }
        public string RellenoFuente { get; set; }
        public decimal? Agl { get; set; }
        public decimal? Cp { get; set; }
        public string Color { get; set; }
        public string Olor { get; set; }
        public string? Observacion { get; set; }
    }
}
