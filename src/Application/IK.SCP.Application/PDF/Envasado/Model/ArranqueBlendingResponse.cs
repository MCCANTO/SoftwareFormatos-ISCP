using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace IK.SCP.Application.PDF.Envasado.Model;

public class ArranqueBlendingResponse
{
    public int BlendingArranqueId { get; set; }
    public string Orden { get; set; }
    public string Producto { get; set; }
    public string Responsable { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Turno { get; set; }
    public int Cerrado { get; set; }

    public List<ComponenteBlending> Componentes { get; set; }
    public List<ArranqueCondicionPreviaBlending> CondicionesPrevias { get; set; }
    public List<VerificacionEquipoBlending> VerificacionesEquipo { get; set; }
    public List<ObservacionBlending> Observaciones { get; set; }
}


public class ComponenteBlending
{
    public string Articulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Porcentaje { get; set; }
}

public class ArranqueCondicionPreviaBlending
{
    public int BlendingArranqueCondicionPreviaId { get; set; }
    public string Nombre { get; set; }
    public string Comentario { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
    public string Orden { get; set; }
}

public class VerificacionEquipoBlending
{
    public string Nombre { get; set; }
    public string Operativo { get; set; }
    public string Limpio { get; set; }
    public string Observacion { get; set; }
}

public class ObservacionBlending
{
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string valor { get; set; }
}