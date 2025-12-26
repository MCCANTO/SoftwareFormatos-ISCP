namespace IK.SCP.Domain.Dtos
{
    public class ControlPefFuerzaCorteAcondCreateDto
    {
        public int ControlTratamientoId { get; set; }
        public List<ControlPefFuerzaCorteDetalleAcondCreateDto>? FuerzaCortes { get; set; }
    }

    public class ControlPefFuerzaCorteDetalleAcondCreateDto
    {
        public int Item { get; set; }
        public decimal? ControlSinPef { get; set; }
        public decimal? Pef { get; set; }
    }
}
