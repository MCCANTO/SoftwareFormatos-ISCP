namespace IK.SCP.Domain.Dtos
{
    public class RemojoMaizControlAcondUpdateDto
    {
        public int? Id { get; set; }
        public string OrdenId { get; set; }
        public int NumeroTanque { get; set; }
        public string? Olor { get; set; }
        public decimal? PhAntes { get; set; }
        public decimal? PhDespues { get; set; }
        public string? InicioAgitacion { get; set; }
        public string? FinAgitacion { get; set; }
        public string? Observacion { get; set; }
    }
}
