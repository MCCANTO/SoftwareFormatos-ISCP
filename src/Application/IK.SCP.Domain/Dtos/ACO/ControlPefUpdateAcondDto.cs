namespace IK.SCP.Domain.Dtos
{
    public class ControlPefUpdateAcondDto
    {
        public int ControlTratamientoId { get; set; }
        public string? Proveedor { get; set; }
        public string? Lote { get; set; }
        public decimal? Humedad { get; set; }
        public decimal? Brix { get; set; }
    }
}
