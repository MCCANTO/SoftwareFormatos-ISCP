namespace IK.SCP.Application.ACO.General.ViewModels;

public class GetByIdReposoMaizPDFResponse
{
    public int numeroTanque { get; set; }
    public int numeroBatch { get; set; }
    
    public List<GetByOrdenReposoMaiz> getByOrdenReposoMaiz { get; set; }
}

public class GetByOrdenReposoMaiz
{
    public int id { get; set; }
    public int numeroBatch { get; set; }
    public double cantidadBatch { get; set; }
    public DateTime fechaHoraInicioReposo { get; set; }
    public DateTime fechaHoraInicioFritura { get; set; }
    public string usuario { get; set; }
    public string observacion { get; set; }
}