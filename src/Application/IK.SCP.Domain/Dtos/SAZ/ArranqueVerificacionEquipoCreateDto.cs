namespace IK.SCP.Domain.Dtos
{

    public class ArranqueVerificacionSazonadoCreateDto
    {
        public int ArranqueVerificacionEquipoId { get; set; }
        public int ArranqueId { get; set; }
        public List<ArranqueVerificacionSazonadoDetalleCreateDto> Verificaciones { get; set; }
    }

    public class ArranqueVerificacionSazonadoDetalleCreateDto
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}
