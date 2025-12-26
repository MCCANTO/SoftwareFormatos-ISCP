namespace IK.SCP.Domain.Dtos
{
    public class PeladoMaizInsumoAcondUpdateDto
    {
        public int PeladoMaizInsumoId { get; set; }
        public string OrdenId { get; set; }
        public string Insumo { get; set; }
        public string Lote { get; set; }
        public decimal? Inicio { get; set; }
        public decimal? Fin { get; set; }
        public string Unidad { get; set; } = "KG";
    }
}
