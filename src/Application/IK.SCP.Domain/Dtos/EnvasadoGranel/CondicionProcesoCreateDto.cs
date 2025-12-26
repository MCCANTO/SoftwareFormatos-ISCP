namespace IK.SCP.Domain.Dtos
{
    public class CondicionProcesoGranelCreateDto
    {
        public int ArranqueGranelCondicionProcesoId { get; set; }
        public int ArranqueGranelId { get; set; }
        public List<CondicionProcesoGranelDetalleCreateDto> Condiciones { get; set; }
    }

    public class CondicionProcesoGranelDetalleCreateDto
    {
        public int Id { get; set; }
        public int CondicionProcesoId { get; set; }
        public string Valor { get; set; } = string.Empty;
        public string Observacion { get; set; } = string.Empty;
    }
}
