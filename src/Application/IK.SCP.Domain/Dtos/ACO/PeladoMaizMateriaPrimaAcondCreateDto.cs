namespace IK.SCP.Domain.Dtos
{
    public class PeladoMaizMateriaPrimaAcondCreateDto
    {
        public string OrdenId { get; set; }
        public string Calidad { get; set; }
        public decimal Cantidad { get; set; }
        public string Lote { get; set; }
    }
}
