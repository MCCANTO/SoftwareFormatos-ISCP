namespace IK.SCP.Domain.Dtos
{
    public class CondicionOperativaGranelDto
    {
        public int Id { get; set; }
        public int? TipoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public List<CondicionOperativaGranelDetalleDto> Detalle { get; set; }
    }

    public class CondicionOperativaGranelDetalleDto
    {
        public int Id { get; set; }
        public int CondicionOperativaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public bool Valor { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public int Secuencia { get; set; }
    }

}
