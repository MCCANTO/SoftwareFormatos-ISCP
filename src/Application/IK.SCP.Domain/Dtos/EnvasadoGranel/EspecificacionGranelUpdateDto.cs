namespace IK.SCP.Domain.Dtos
{
    public class EspecificacionGranelUpdateDto
    {
        public int Id { get; set; }
        public int ArranqueGranelId { get; set; }
        public int EspecificacionId { get; set; }
        public int Valor { get; set; }
        public string Otro { get; set; }
    }
}
