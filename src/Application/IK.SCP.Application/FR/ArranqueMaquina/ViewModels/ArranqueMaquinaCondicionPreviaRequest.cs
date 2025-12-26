namespace IK.SCP.Application.FR.ViewModels
{
    public class ArranqueMaquinaCondicionPreviaRequest
    {
        public int ArranqueMaquinaCondicionPreviaId { get; set; }
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string Observacion { get; set; }
    }

    public class ArranqueMaquinaVariableBasicaRequest
    {
        public int ArranqueMaquinaVariableBasicaId { get; set; }
        public int VariableBasicaId { get; set; }
        public bool Valor { get; set; }
        public string Observacion { get; set; }
    }
}