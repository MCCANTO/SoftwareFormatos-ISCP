namespace IK.SCP.Domain.Dtos
{
    public class ControlPefTiempoAcondCreateDto
    {
        public int ControlTratamientoId { get; set; }
        public int NumeroPaleta { get; set; }
        public decimal? CantidadKg { get; set; }
        public string? HoraInicioPef { get; set; }
        public string? HoraInicioFritura { get; set; }
        public string? Observacion { get; set; }
    }
}
