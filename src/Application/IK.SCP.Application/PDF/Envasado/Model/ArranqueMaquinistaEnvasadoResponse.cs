using iText.Layout.Element;

namespace IK.SCP.Application.PDF.Envasado.Model;

public class ArranqueMaquinistaEnvasadoResponse
{
    public int ArranqueMaquinaId { get; set; }
    public decimal? PesoSobreProducto1 { get; set; }
    public decimal? PesoSobreProducto2 { get; set; }
    public decimal? PesoSobreProducto3 { get; set; }
    public decimal? PesoSobreProducto4 { get; set; }
    public decimal? PesoSobreProducto5 { get; set; }
    public decimal? PesoSobreProductoProm { get; set; }
    public decimal? PesoSobreVacio { get; set; }
    public string? Observacion { get; set; }
    public string? UsuarioCreacion { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public string? Articulo { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? OrdenId { get; set; }
    public int? EnvasadoraId { get; set; }
    public bool EsReempaque { get; set; }

    public List<CondicionesPrevias> condicionesPrevias{ get; set; }

    public List<FechaVariableBasica> fechaVariableBasica { get; set; }
    public List<VariablesBasicas> variablesBasicas { get; set; }
    public List<ArranqueMaquinaObservacionViewModel> observaciones { get; set; }
}

public class CondicionesPrevias
{
    public int Orden { get; set; }
    public string Descripcion { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
}

public class VariablesBasicas
{
    public string? Padre { get; set; }
    public string? Nombre { get; set; }
    public string? Inicio { get; set; }
    public string? Intermedio { get; set; }
    public string? Final { get; set; }
    public string? Observaciones { get; set; }
}
public class FechaVariableBasica
{
    public int id { get; set; }
    public DateTime fecha { get; set; }
}

public class ArranqueMaquinaObservacionViewModel
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Observacion { get; set; }
}