namespace IK.SCP.Domain.Dtos
{
    public class OrdenFrituraDto
    {
        public string Orden { get; set; }
        public int LineaProduccionId { get; set; }
        public string Linea { get; set; }
        public string Producto { get; set; }
    }
}
