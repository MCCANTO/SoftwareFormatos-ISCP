namespace IK.SCP.Domain.Dtos
{
    public class CondicionProcesoGranelDto
    {
        public int Id { get; set; }
        public int CondicionProcesoId { get; set; }
        public string Padre { get; set; }
        public string Nombre { get; set; }
        public string? Comentario { get; set; }
        public int PrimerOrden { get; set; }
        public int SegundoOrden { get; set; }
        public string? Valor { get; set; }
        public string Observacion { get; set; }
        public bool Cerrado { get; set; }
    }
}
