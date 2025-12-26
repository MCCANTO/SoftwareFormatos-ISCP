namespace IK.SCP.Application.PDF.Fritura.Model;

public class ControlPeladoRemojoSancochadoResponse
{
    public string Orden { get; set; }
    public DateTime Fecha { get; set; }
    public string Usuario { get; set; }
    public string MateriaPrima { get; set; }
    public List<MateriaPrimaConsumo> listaMateriaPrimaConsumos { get; set; }
    public List<InsumosConsumo> listaInsumoConsumos { get; set; }
    public List<ObservacionConsumo> listaObservacionConsumos { get; set; }
    public Pelado? pelado { get; set; }
    public Remojo? remojo { get; set; }
    public Sancochado? sancochado { get; set; }
}

public class MateriaPrimaConsumo
{
    public string calidad { get; set; }
    public int cantidad { get; set; }
    public string lote { get; set; }
}
public class InsumosConsumo
{
    public int peladoMaizInsumoId { get; set; }
    public string insumo { get; set; }
    public string lote { get; set; }
    public string Unidad { get; set; }
    public decimal cantidadInicio { get; set; }
    public decimal cantidadFinal { get; set; }
    public decimal consumo { get; set; }
}
public class ObservacionConsumo
{
    public DateTime fechaHora { get; set; }
    public string usuario { get; set; }
    public string observacion { get; set; }
}



public class Pelado
{
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public List<PeladoDetail> listaPelado { get; set; }
}

public class PeladoDetail
{
    public int id { get; set; }
    public string numeroBatch { get; set; }
    public DateTime fechaHoraInicio { get; set; }
    public string temperaturaInicio { get; set; }
    public DateTime fechaHoraFin { get; set; }
    public string temperaturaFin { get; set; }
    public string cal { get; set; }
    public string numeroTanque { get; set; }
    public string observacion { get; set; }
    public string cerrado { get; set; }
}

public class Remojo
{
    public string numeroTanque { get; set; }
    public DateTime fechaInicio { get; set; }
    public DateTime fechaFin { get; set; }
    public List<Lavado> ListaLavado { get; set; }
}

public class Lavado
{
    public string numeroTanque { get; set; }
    public string olor { get; set; }
    public string olorDesc { get; set; }
    public string phAntes { get; set; }
    public string phDespues { get; set; }
    public DateTime inicioAgitacion { get; set; }
    public DateTime finAgitacion { get; set; }
    public string observacion { get; set; }
}

public class Sancochado
{
    public DateTime fechaInicio { get; set; }
    public DateTime fechaFinal { get; set; }
    public List<SancochadoDetail> ListaSancochado { get; set; }
}

public class SancochadoDetail
{
    public string numeroTanque { get; set; }
    public string numeroBatch { get; set; }
    public DateTime fechaHoraInicio { get; set; }
    public string temperaturaInicio { get; set; }
    public DateTime fechaHoraFin { get; set; }
    public string temperaturaFin { get; set; }
    public string observacion { get; set; }
}