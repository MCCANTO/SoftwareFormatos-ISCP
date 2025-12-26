namespace IK.SCP.Domain.Dtos
{
    public class ArranqueLavadoTuberculoVerificacionAcondCreateDto
    {
        public int ArranqueLavadoTuberculoVerificacionEquipoId { get; set; }
        public int ArranqueLavadoTuberculoId { get; set; }
        public List<ArranqueLavadoTuberculoVerificacionDetalleAcondCreateDto>? Verificaciones { get; set; }
    }

    public class ArranqueLavadoTuberculoVerificacionDetalleAcondCreateDto
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}