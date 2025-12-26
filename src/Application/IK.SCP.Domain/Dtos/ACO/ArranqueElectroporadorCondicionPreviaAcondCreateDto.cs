namespace IK.SCP.Domain.Dtos
{
    public class ArranqueElectroporadorCondicionBasicaAcondCreateDto
    {
        public int ArranqueElectroporadorId { get; set; }
        public List<ArranqueElectroporadorCondicionAcondDetalleCreateDto>? Condiciones { get; set; }
    }

    public class ArranqueElectroporadorCondicionAcondDetalleCreateDto
    {
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
