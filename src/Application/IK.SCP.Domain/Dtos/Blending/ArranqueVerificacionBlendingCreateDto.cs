namespace IK.SCP.Domain.Dtos
{
    public class ArranqueVerificacionBlendingCreateDto
    {
        public int ArranqueVerificacionEquipoId { get; set; }
        public int BlendingArranqueId { get; set; }
        public List<ArranqueVerificacionBlendingDetalleCreateDto> Verificaciones { get; set; }
    }

    public class ArranqueVerificacionBlendingDetalleCreateDto
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}
