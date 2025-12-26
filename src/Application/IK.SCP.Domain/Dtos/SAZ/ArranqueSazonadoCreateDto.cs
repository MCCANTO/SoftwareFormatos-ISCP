namespace IK.SCP.Domain.Dtos
{

    public class ArranqueSazonadoCreateDto
    {
        public int SazonadorId { get; set; }
        public string Sabor { get; set; }
        public string? Otro { get; set; }
        public List<ArranqueProductoSazonadoCreateDto> Ordenes { get; set; }
    }

    public class ArranqueProductoSazonadoCreateDto
    {
        public int LineaProduccionId { get; set; }
        public string Orden { get; set; }
        public string Producto { get; set; }
    }

}
