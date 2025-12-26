namespace IK.SCP.Application.FR.ViewModels;

public class GetByIdEvaluacionAtributoPDFResponse
{
    public DateTime Fecha { get; set; }
    public string Turno { get; set; }
    public int Linea { get; set; }
    public string Descripcion { get; set; }
    public string Orden { get; set; }
    public string Maquinista { get; set; }
    public List<GetByOrdenEvaluacionAtributo> EvaluacionAtributos { get; set; }
}

public class GetByOrdenEvaluacionAtributo
{
    public string HoraMinutoSegundo { get; set; }
    public string Panelistas { get; set; }
    public int AparienciaGeneral { get; set; }
    public int Color { get; set; }
    public int Olor { get; set; }
    public int sabor { get; set; }
    public int Textura { get; set; }
    public int CalificacionFinal { get; set; }
    public string Observacion { get; set; }
    public int NumPanelistas { get; set; }
}