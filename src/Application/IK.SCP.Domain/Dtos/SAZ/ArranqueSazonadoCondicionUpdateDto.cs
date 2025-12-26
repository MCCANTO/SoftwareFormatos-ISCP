namespace IK.SCP.Domain.Dtos
{
    public class ArranqueSazonadoCondicionUpdateDto
    {
        public int ArranqueId { get; set; }
        public List<ArranqueSazonadoCondicionUpdateDetalleDto> Condiciones { get; set; }
    }

    public class ArranqueSazonadoCondicionUpdateDetalleDto
    {
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
    }
}
