using IK.SCP.Application.PDF.Fritura.Model;

namespace IK.SCP.Application.PDF.Sazonado.Model;

public class ChecklistArranqueSazonadoResponse
{
    public string Sabor { get; set; }
    public string Responsable { get; set; }
    public string Encargado { get; set; }
    public string Turno { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int PesoInicio { get; set; }
    public string ObservacionInicio { get; set; }
    public int PesoFin { get; set; }
    public string ObservacionFin { get; set; }
    
    public ArranqueProducto arranque { get; set; }
    public List<CondicionesPreviasSazonado> listaCondicionesPrevias { get; set; }
    public List<VerificacionEquipoSazonado> listaVerificacionEquipo { get; set; }
    
}

public class ArranqueProducto
{
    public string Linea { get; set; }
    public string Orden { get; set; }
    public string Producto { get; set; }
}

public class CondicionesPreviasSazonado
{
    public string Orden { get; set; }
    public string Nombre { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
}

public class VerificacionEquipoSazonado
{
    public int Id { get; set; }
    public int VerificacionEquipoId { get; set; }
    public int Orden_1 { get; set; }
    public string Nombre_1 { get; set; }
    public string Comentario_1 { get; set; }
    public int Orden_2 { get; set; }
    public string Nombre_2 { get; set; }
    public string Comentario_2 { get; set; }
    public int Orden_3 { get; set; }
    public string Nombre_3 { get; set; }
    public string Comentario_3 { get; set; }
    public string Operativo { get; set; }
    public string Limpio { get; set; }
    public int Cerrado { get; set; }
    public string Observacion { get; set; }
    public int rowSpan { get; set; }
}