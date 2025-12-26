namespace IK.SCP.Domain.Dtos
{
    public class ControlMermaBlendingUpdateDto
    {
        public int Id { get; set; }
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public decimal? Merma { get; set; }
    }
}
