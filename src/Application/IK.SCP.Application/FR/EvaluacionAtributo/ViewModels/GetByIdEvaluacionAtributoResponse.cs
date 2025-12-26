namespace IK.SCP.Application.FR.ViewModels
{
    public class GetByIdEvaluacionAtributoResponse
    {
        public int AparienciaGeneral { get; set; }
        public int Color { get; set; }
        public int Olor { get; set; }
        public int Sabor { get; set; }
        public int Textura { get; set; }
        public int CalificacionFinal { get; set; }
        public string Observacion { get; set; }
        public List<GetByIdEvaluacionAtributoPanelistaResponse> Panelistas { get; set; }
    }

    public class GetByIdEvaluacionAtributoPanelistaResponse
    {
        public int PanelistaId { get; set; }
        public string Nombre { get; set; }
    }
}
