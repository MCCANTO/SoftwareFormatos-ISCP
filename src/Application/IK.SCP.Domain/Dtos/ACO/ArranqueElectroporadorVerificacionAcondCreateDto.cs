namespace IK.SCP.Domain.Dtos
{
    public class ArranqueElectroporadorVerificacionAcondCreateDto
    {
        public int ArranqueElectroporadorVerificacionEquipoId { get; set; }
        public int ArranqueElectroporadorId { get; set; }
        public List<ArranqueElectroporadorVerificacionDetalleAcondCreateDto>? Verificaciones { get; set; }
    }

    public class ArranqueElectroporadorVerificacionDetalleAcondCreateDto
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}
