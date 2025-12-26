namespace IK.SCP.Domain.Dtos
{
    public class EnvasadoRegistroPedaceriaCreateDto
    {
        public int EnvasadoraId { get; set; }
        public string OrdenId { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Pedaceria { get; set; }
        public decimal? PorcentajePedaceria { get; set; }
        public decimal? HojuelasEnteras { get; set; }
        public decimal? PorcentajeHojuelasEnteras { get; set; }
        public string? Inspector { get; set; }
        public string? Observacion { get; set; }
    }
}
