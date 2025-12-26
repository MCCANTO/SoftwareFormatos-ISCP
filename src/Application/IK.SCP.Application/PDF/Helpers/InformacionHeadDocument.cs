namespace IK.SCP.Application.PDF.Helpers;

public class InformacionHeadDocument
{
    public string Code { get; set; }
    public string Descripcion { get; set; }
    public string Version { get; set; }
    public string Fecha { get; set; }

    public InformacionHeadDocument(string code, string descripcion, string version, string fecha)
    {
        Code = code;
        Descripcion = descripcion;
        Version = version;
        Fecha = fecha;
    }
}