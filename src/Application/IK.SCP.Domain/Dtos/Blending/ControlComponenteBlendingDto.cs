namespace IK.SCP.Domain.Dtos
{
    public class ControlComponenteBlendingDto
    {
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Peso { get; set; }
        public string? Lote { get; set; }
    }
}
