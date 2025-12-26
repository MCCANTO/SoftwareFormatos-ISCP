namespace IK.SCP.Domain.Dtos
{
    public class OrdenAcondCreateDto
    {
        public string OrdenId { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public int MateriaPrimaId { get; set; }
    }
}
