namespace IK.SCP.Application.PDF.Fritura.Model;

public class ArranqueManufacturaResponse
{
    public int ArranqueMaquinaId { get; set; }
    public string Responsable { get; set; }
    public DateTime Fecha { get; set; }
    public string Orden { get; set; }
    public string Articulo { get; set; }
    public string Turno { get; set; }
    public string Encargado { get; set; }
    public bool Cerrado { get; set; }
    public bool Revisado { get; set; }

    public List<Condicion> Condiciones { get; set; }
    public VerificacionEquipo Verificaciones { get; set; }
    public List<Observacion> Observaciones { get; set; }
    public List<Sensorial> Sensoriales { get; set; }
}

public class Condicion
{
    public int ArranqueMaquinaCondicionPreviaId { get; set; }
    public int CondicionPreviaId { get; set; }
    public string Nombre { get; set; }
    public string Comentario { get; set; }
    public bool Valor { get; set; }
    public int Orden { get; set; }
}

public class VerificacionEquipo
{
    public int ArranqueMaquinaVerificacionEquipoCabId { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public bool Cerrado { get; set; }
    public List<VerificacionPreviaArranque> VerificacionesArranque { get; set; }
}

public class VerificacionPreviaArranque
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

public class Observacion
{
    public int ArranqueMaquinaObservacionId { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Observaciones { get; set; }
}

public class Sensorial
{
    public int AparienciaGeneral { get; set; }
    public int Color { get; set; }
    public int Olor { get; set; }
    public int Sabor { get; set; }
    public int Textura { get; set; }
    public int CalificacionFinal { get; set; }
    public string Panelistas { get; set; }
    public string Observacion { get; set; }
}