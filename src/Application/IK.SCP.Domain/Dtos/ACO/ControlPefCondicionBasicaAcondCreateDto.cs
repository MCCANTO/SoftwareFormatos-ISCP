namespace IK.SCP.Domain.Dtos
{
    public class ControlPefCondicionBasicaAcondCreateDto
    {
        public int ControlTratamientoId { get; set; }
        public int TipoId { get; set; }
        public List<ControlPefCondicionDetalleAcondCreateDto>? Condiciones { get; set; }
    }

    public class ControlPefCondicionDetalleAcondCreateDto
    {
        public int CondicionPreviaId { get; set; }
        public decimal? Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
