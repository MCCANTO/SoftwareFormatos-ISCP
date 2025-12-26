namespace IK.SCP.Application.PDF.Fritura.Model;

public class ChecklistArranqueElectroporadorResponse
{
    public string MateriaPrima { get; set; }
    public string Responsable { get; set; }
    public string Encargado { get; set; }
    public DateTime Fecha { get; set; }
    public string Turno { get; set; }
    
    public List<CondicionesBasicas> listaCondicionesBasicas { get; set; }
    public List<VerificacionEquipoElectroporador> listaVerificacionEquipo { get; set; }
    public List<VariablesBasicas> listaVariablesBasicas { get; set; }
}

public class CondicionesBasicas
{
    public int Orden { get; set; }
    public string Descripcion { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
}

public class VerificacionEquipoElectroporador
{
    public string Descripcion { get; set; }
    public string Operativo { get; set; }
    public string Limpio { get; set; }
    public string Observacion { get; set; }
}

public class VariablesBasicas
{
    public int Orden { get; set; }
    public string Descripcion { get; set; }
    public int Valor { get; set; }
    public string Observacion { get; set; }
}