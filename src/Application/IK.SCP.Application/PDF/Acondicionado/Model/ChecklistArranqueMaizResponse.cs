namespace IK.SCP.Application.PDF.Fritura.Model;

public class ChecklistArranqueMaizResponse
{
    public string MateriaPrima { get; set; }
    public string Responsable { get; set; }
    public string Encargado { get; set; }
    public DateTime Fecha { get; set; }
    public string Turno { get; set; }
    public string Temperatura { get; set; }
    public string ObservacionTemperatura { get; set; }
    
    public List<CondicionesPreviasMaiz> listaCondicionesPrevias { get; set; }
    public List<AcondicionamientoArranqueVerificacionEquipoMaiz> listaVerificacionEquipo { get; set; }
}

public class CondicionesPreviasMaiz
{
    public int Orden { get; set; }
    public string Descripcion { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
}

public class AcondicionamientoArranqueVerificacionEquipoMaiz
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