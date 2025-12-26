namespace IK.SCP.Domain.Dtos
{

    public class GetAllChecklistGranelDto
    {
        public int ArranqueGranelId { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ChecklistGranelDto
    {
        public int ArranqueGranelId { get; set; }
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public string? TipoId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? LineaFrituraId { get; set; }
        public string? Maquinista { get; set; }
        public string? Selladora { get; set; }
        public string? PersonalPesa { get; set; }
        public string? PersonalSella { get; set; }
        public bool Cerrado { get; set; }
        public bool Revisado { get; set; }
        public List<EspecificacionChecklistGranelDto> Especificaciones { get; set; }
        public List<CondicionOperativaChecklistGranelDto> CondicionesOperativas { get; set; }
        public List<CondicionProcesoChecklistGranelDto> CondicionesProceso { get; set; }
        public List<ObservacionChecklistGranelDto> Observaciones { get; set; }
        public List<dynamic> Revisiones { get; set; }

    }

    public class EspecificacionChecklistGranelDto
    {
        public int Id { get; set; }
        public int EspecificacionId { get; set; }
        public int? Valor { get; set; }
        public string Descripcion { get; set; }
    }

    public class CondicionOperativaChecklistGranelDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CondicionProcesoChecklistGranelDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrado { get; set; }
    }

    public class ObservacionChecklistGranelDto
    {
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }

    public class ChecklistGranelUpdateDto
    {
        public int ArranqueGranelId { get; set; }
        public string? TipoId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? LineaFrituraId { get; set; }
        public string? Maquinista { get; set; }
        public string? Selladora { get; set; }
        public string? PersonalPesa { get; set; }
        public string? PersonalSella { get; set; }

    }
}
