namespace IK.SCP.Domain.Dtos.ENV
{
    public class ArranqueMaquinaCreateDto
    {
    }

    public class ArranqueMaquinaCondicionPreviaCreateDto
    {
        public int arranqueMaquinaId { get; set; }
        public int? tipoId { get; set; }
        public List<ArranqueMaquinaCondicionPreviaItemCreateDto>? condiciones { get; set; }
    }

    public class ArranqueMaquinaCondicionPreviaItemCreateDto
    {
        public int? condicionPreviaId { get; set; }
        public bool valor { get; set; }
        public string? observacion { get; set; }
    }

    public class ArranqueMaquinaVariableBasicaCreateDto
    {
        public int? arranqueMaquinaVarBasCabId { get; set; }
        public int? arranqueMaquinaId { get; set; }
        public List<ArranqueMaquinaVariableBasicaItemCreateDto>? variables { get; set; }
    }

    public class ArranqueMaquinaVariableBasicaItemCreateDto
    {
        public int? id { get; set; }
        public int? variableBasicaId { get; set; }
        public string? valor { get; set; }
        public string? observacion { get; set; }
    }

    public class ArranqueMaquinaObservacionCreateDto
    {
        public string? observacion { get; set; }
    }
}
