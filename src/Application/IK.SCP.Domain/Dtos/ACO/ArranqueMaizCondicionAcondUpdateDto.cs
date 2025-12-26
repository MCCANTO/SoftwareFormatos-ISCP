namespace IK.SCP.Domain.Dtos
{
    public class ArranqueMaizCondicionAcondUpdateDto
    {
        public int ArranqueMaizId { get; set; }
        public List<ArranqueMaizCondicionAcondDetalleUpdateDto> Condiciones { get; set; }
    }

    public class ArranqueMaizCondicionAcondDetalleUpdateDto
    {
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
