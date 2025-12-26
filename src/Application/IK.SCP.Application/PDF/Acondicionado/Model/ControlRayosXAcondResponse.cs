namespace IK.SCP.Application.PDF.Fritura.Model;

public class ControlRayosXAcondResponse
{
    public string Mes { get; set; }
    public string Año { get; set; }
    
    public List<ControlMonitoreo> listaControlMonitoreo { get; set; }
}

public class ControlMonitoreo
{
    public string ControlRayosXId { get; set; }
    public DateTime FechaHora { get; set; }
    public string MateriaPrimaId { get; set; }
    public string MateriaPrima { get; set; }
    public string DeteccionUno { get; set; }
    public string DeteccionDos { get; set; }
    public string Conformidad { get; set; }
    public string ConformidadDesc { get; set; }
    public string UsuarioEjecucion { get; set; }
    public string Observacion { get; set; }
    public string Revisado { get; set; }
    public string FechaHoraRevision { get; set; }
    public string UsuarioRevision { get; set; }
}