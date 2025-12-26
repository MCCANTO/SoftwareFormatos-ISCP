namespace IK.SCP.Domain.Dtos
{
    public class ArranqueCondicionBlendingUpdateDto
    {
        public int BlendingArranqueId { get; set; }
        public List<ArranqueCondicionBlendingUpdateDetalleDto> Condiciones { get; set; }
    }

    public class ArranqueCondicionBlendingUpdateDetalleDto
    {
        public int BlendingArranqueCondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
