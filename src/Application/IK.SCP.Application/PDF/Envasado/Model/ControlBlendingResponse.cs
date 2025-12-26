namespace IK.SCP.Application.PDF.Envasado.Model;

public class ControlBlendingResponse
{
    public string  Orden { get; set; }
    public DateTime  Fecha { get; set; }
    public string  Turno { get; set; }
    public string  Producto { get; set; }
    public string  Maquinista { get; set; }
    
    public List<ComponenteMix> componentes { get; set; }
    public List<HeadTableBlending> headTable { get; set; }
    public List<MermaBlending> merma { get; set; }
    public List<dynamic> dataTable { get; set; }
}

public class ComponenteMix
{
    public string  Articulo { get; set; }
    public string  Descripcion { get; set; }
    public decimal Porcentaje { get; set; }
    public string  Granel { get; set; }
    public string  Linea { get; set; }
    public string  OrdenFritura { get; set; }
}

public class HeadTableBlending
{
    public string Articulo { get; set; }
    public string Descripcion { get; set; }
}

public class MermaBlending
{
    public int Id { get; set; }
    public string Articulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Merma { get; set; }
}

