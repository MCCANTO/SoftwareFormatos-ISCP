namespace IK.SCP.Domain.Dtos
{
    public class CondicionOperativaGranelCreateDto
    {
        public int ArranqueGranelId { get; set; }
        public int TipoId { get; set; }
        public List<CondicionOperativaGranelDetalleCreateDto>? Condiciones { get; set; }
    }

    public class CondicionOperativaGranelDetalleCreateDto
    {
        public int CondicionOperativaId { get; set; }
        public bool? Valor { get; set; }
        public string Observacion { get; set; } = string.Empty;
    }
}