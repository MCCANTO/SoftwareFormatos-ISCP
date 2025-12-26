namespace IK.SCP.Application.PDF.Fritura.Model;

public class ControlReposoRemojoResponse
{
    public string responsable { get; set; }
    public int numeroTanque { get; set; }
    public int numeroBatch { get; set; }
    public List<ControlReposoRemojoDetail>? ListaControlReposoRemojo { get; set; }
}

public class ControlReposoRemojoDetail
{
    public int id { get; set; }
    public int numeroBatch { get; set; }
    public double cantidadBatch { get; set; }
    public DateTime fechaHoraInicioReposo { get; set; }
    public DateTime fechaHoraInicioFritura { get; set; }
    public string usuario { get; set; }
    public string observacion { get; set; }
}