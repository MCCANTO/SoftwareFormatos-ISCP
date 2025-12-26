namespace IK.SCP.Domain.Dtos
{
    public class CodificacionCajaGranelCreateDto
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public decimal Tamanio { get; set; }
        public string TipoArchivo { get; set; }
    }
}
