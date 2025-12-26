using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace IK.SCP.Application.PDF.Envasado.Model;

public class ArranqueControlProcesosE4Response
{
    public string Orden { get; set; }
    public string DescripcionProducto { get; set; }
    public DateTime Fecha { get; set; }
    public string LineaFritura { get; set; }
    public string Turno { get; set; }
    public string Selladora { get; set; }
    public DateTime FechaEnvasado { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Maquinista { get; set; }
    public string UsuarioCreacion { get; set; }
    public string PersonalPesa { get; set; }
    public string PersonalSella { get; set; }
    public string Tipo { get; set; }

    public List<dynamic> EspecificacionesEnvasadoGranel { get; set; }
    public List<CondicionesOperativasEnvasadoGranel> CondicionesOperativas { get; set; }
    public List<CondicionesProcesoEnvasadoGranel> CondicionesProceso { get; set; }
    public List<ObservacionesEnvasadoGranel> Observacion { get; set; }
    public ControlProcesoEnvasadoGranel ControlProceso { get; set; }
    public List<dynamic> ObservacionControlProceso { get; set; }
    public List<evaluacionPTControlProceso> EvaluacionAtributos { get; set; }

    public ControlParametroGranelProcesoE4 ControlParametroGranelProcesoE4 { get; set; }
    public List<dynamic> ImgCodificacionCaja { get; set; }

    public List<turnosE4CP> TurnosE4Cps { get; set; }
}

public class turnosE4CP
{
    public string Turno { get; set; }
    public string Fechas { get; set; }
}

public class CondicionesOperativasEnvasadoGranel
{
    public string Id { get; set; }
    public string CondicionesOperativaId { get; set; }
    public string Nombre { get; set; }
    public bool Valor { get; set; }
    public string Observacion { get; set; }
    public string Secuencia { get; set; }
}

public class CondicionesProcesoEnvasadoGranel
{
    public string Padre { get; set; }
    public string Nombre { get; set; }
    public string Valor1 { get; set; }
    public string Valor2 { get; set; }
    public string Valor3 { get; set; }
    public string? Valor4 { get; set; }
    public string Observacion { get; set; }
}

public class ObservacionesEnvasadoGranel
{
    public string valor { get; set; }
}

public class ControlProcesoEnvasadoGranel
{
    public List<DateTime> FechaControlProceso { get; set; }
    public List<dynamic> DetalleControlProceso { get; set; }
}
// public class ObservacionControlProceso
// {
//     public int Id { get; set; }
//     public string Observacion { get; set; }
// }

public class evaluacionPTControlProceso
{
    public string HoraMinutoSegundo { get; set; }
    public string Panelistas { get; set; }
    public int Apariencia { get; set; }
    public int Color { get; set; }
    public int Olor { get; set; }
    public int sabor { get; set; }
    public int Textura { get; set; }
    public int CalificacionFinal { get; set; }
    public string Observacion { get; set; }
    public int NumPanelistas { get; set; }
}

public class ControlParametroGranelProcesoE4
{
    public List<DateTime>? Cabeceras { get; set; }
    public List<dynamic>? Controles { get; set; }
}