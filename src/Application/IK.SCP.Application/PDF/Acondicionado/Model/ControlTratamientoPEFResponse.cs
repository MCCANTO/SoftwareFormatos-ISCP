namespace IK.SCP.Application.PDF.Fritura.Model;

public class ControlTratamientoPEFResponse
{
    public string proveedor { get; set; }
    public string lote { get; set; }
    public int humedad { get; set; }
    public int brix { get; set; }
    public DateTime FechaEjecucion { get; set; }
    public string Nombre { get; set; }
    public List<CondicionPrevia> ListaCondicionesPrevias { get; set; }
    public List<FuerzaCortes> ListaFuerzaCortes { get; set; }
    public List<Tiempos> ListaTiempos { get; set; }
    
}

public class FuerzaCortes
{
    public int Secuencial { get; set; }
    public decimal ControlSinPef_1 { get; set; }
    public decimal Pef_1 { get; set; }
    public decimal ControlSinPef_2 { get; set; }
    public decimal Pef_2 { get; set; }
    public decimal ControlSinPef_3 { get; set; }
    public decimal Pef_3 { get; set; }
}

public class Tiempos
{
    public int numeroPaleta { get; set; }
    public int cantidadKg { get; set; }
    public string horaInicioPef { get; set; }
    public string horaInicioFritura { get; set; }
    public string observacion { get; set; }
}

public class CondicionPrevia
{
    public string Nombre { get; set; }
    public decimal Valor_1 { get; set; }
    public string Obs_1 { get; set; }
    public decimal Valor_2 { get; set; }
    public string Obs_2 { get; set; }
    public decimal Valor_3 { get; set; }
    public string Obs_3 { get; set; }
}