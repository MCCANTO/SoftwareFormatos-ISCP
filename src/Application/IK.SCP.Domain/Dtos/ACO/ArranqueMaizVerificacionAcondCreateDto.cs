namespace IK.SCP.Domain.Dtos
{
    public class ArranqueMaizVerificacionAcondCreateDto
    {
        public int ArranqueMaizVerificacionEquipoId { get; set; }
        public int ArranqueMaizId { get; set; }
        public List<ArranqueMaizVerificacionDetalleAcondCreateDto> Verificaciones { get; set; }
    }

    public class ArranqueMaizVerificacionDetalleAcondCreateDto
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}