namespace IK.SCP.Application.ENV.ViewModels
{
    public class GetGranelChecklistOrdenRequest
    {
        public int ArranqueGranelId { get; set; }
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public int TipoProductoId { get; set; }
        public bool Cerrado { get; set; }
        public bool Revisado { get; set; }
        public List<GranelEspecificacionRequest> Especificaciones { get; set; }
        public List<GranelCondicionOperativaRequest> CondicionesOperativas { get; set; }
        public List<GranelCondicionProcesoRequest> CondicionesProceso { get; set; }
        public List<GranelObservacionRequest> Observaciones { get; set; }

    }

    public class GranelEspecificacionRequest
    {
        public int Id { get; set; }
        public int EspecificacionId { get; set; }
        public int? Valor { get; set; }
        public string Descripcion { get; set; }
    }
    
    public class GranelCondicionOperativaRequest
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class GranelCondicionProcesoRequest
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrado { get; set; }
    }

    public class GranelObservacionRequest
    {
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
