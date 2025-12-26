namespace IK.SCP.Domain.Dtos
{
    public class ArranqueVariableBasicaSazonadoUpdateDto
    {
        public int ArranqueId { get; set; }
        public decimal? PesoInicio { get; set; }
        public string? ObservacionInicio { get; set; }
        public decimal? PesoFin { get; set; }
        public string? ObservacionFin { get; set; }
    }
}
