namespace IK.SCP.Domain.Dtos
{
    public class PeladoMaizControlAcondUpdateDto
    {
        public int? Id { get; set; }
        public string OrdenId { get; set; }
        public int NumeroBatch { get; set; }
        public decimal? Cal { get; set; }
        public decimal? TemperaturaInicio { get; set; }
        public decimal? TemperaturaFin { get; set; }
        public int? NumeroTanque { get; set; }
        public string? Observacion { get; set; }
    }
}
