namespace IK.SCP.Application.PDF.Envasado.Model;

public class ArranqueEnvasadoResponse
{
    public string DescripcionProducto { get; set; }
    public string Orden { get; set; }
    public string Fecha { get; set; }
    public string Linea { get; set; }
    public string NameEnvasadora { get; set; }
    public bool esReempaque { get; set; }

    public List<CondicionesPreviasArranqueEnvasado> condicionPrevia { get; set; }
    public List<VariablesBasicasArranqueEnvasado> VariablesBasicas { get; set; }
    public List<ImagenCodificacionSobre> ImagenesSobres { get; set; }
    public List<ImagenCodificacionCaja> ImagenesCajas { get; set; }
    public List<CantidadesCajaSobre> CantidadesValores { get; set; }
    public List<dynamic> empacadorPaletizador { get; set; }
    public List<ObservacionesArranque> Observaciones { get; set; }
    public List<EvaluacionSensorialArranqueComponente> EvaluacionSensorial { get; set; }
    public List<InspeccionEtiquetadoArranqueEnvasado> InspeccionEtiquetado { get; set; }
    public List<dynamic> revisores { get; set; }
    public List<ResponsablesVariablesBasicas> responsablesVarBasicas { get; set; }
}

public class ResponsablesVariablesBasicas
{
    public string Turno { get; set; }
    public string Fechas { get; set; }
    public string Maquinistas { get; set; }
}

public class CondicionesPreviasArranqueEnvasado
{
    public int Orden { get; set; }
    public string Nombre { get; set; }
    public bool Valor { get; set; }
}

public class VariablesBasicasArranqueEnvasado
{
    public string Padre { get; set; }
    public string Nombre { get; set; }
    public string Valor { get; set; }
    public string Observacion { get; set; }
    public int RowSpan { get; set; }
}

public class ImagenCodificacionSobre
{
    public string Nombre { get; set; }
    public string Ruta { get; set; }
    public string TipoArchivo { get; set; }
}

public class ImagenCodificacionCaja
{
    public string Nombre { get; set; }
    public string Ruta { get; set; }
    public string TipoArchivo { get; set; }
}

public class CantidadesCajaSobre
{
    public string Valores { get; set; }
}

public class ObservacionesArranque
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Observacion { get; set; }
}

public class EvaluacionSensorialArranqueComponente
{
    public DateTime FechaCreacion { get; set; }
    public string Componente { get; set; }
    public string Lote { get; set; }
    public string Humedad { get; set; }
    public string EvaluacionSensorial { get; set; }
    public string Observacion { get; set; }
    public string UsuarioCreacion { get; set; }
}

public class InspeccionEtiquetadoArranqueEnvasado
{
    public DateTime FechaCreacion { get; set; }
    public string CantidadCaja { get; set; }
    public string Etiquetador { get; set; }
    public string Posicion { get; set; }
    public string Inspector { get; set; }
    public string UsuarioCreacion { get; set; }
    public string Imagen { get; set; }
}