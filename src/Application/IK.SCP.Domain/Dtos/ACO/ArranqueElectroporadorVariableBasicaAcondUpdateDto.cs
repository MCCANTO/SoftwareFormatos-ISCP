namespace IK.SCP.Domain.Dtos
{
    public class ArranqueElectroporadorVariableBasicaAcondUpdateDto
    {
        public int ArranqueElectroporadorId { get; set; }
        public List<ArranqueElectroporadorVariableBasicaDetalleAcondUpdateDto>? Variables { get; set; }
    }

    public class ArranqueElectroporadorVariableBasicaDetalleAcondUpdateDto
    {
        public int VariableBasicaId { get; set; }
        public decimal? Valor { get; set; }
        public string? Observacion { get; set; }
    }
}
