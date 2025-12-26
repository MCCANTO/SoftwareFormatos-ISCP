namespace IK.SCP.Domain.Dtos
{
    public class ArranqueLavadoTuberculoCondicionAcondUpdateDto
    {
        public int ArranqueLavadoTuberculoId { get; set; }
        public List<ArranqueLavadoTuberculoCondicionAcondDetalleUpdateDto>? Condiciones { get; set; }
    }

    public class ArranqueLavadoTuberculoCondicionAcondDetalleUpdateDto
    {
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
