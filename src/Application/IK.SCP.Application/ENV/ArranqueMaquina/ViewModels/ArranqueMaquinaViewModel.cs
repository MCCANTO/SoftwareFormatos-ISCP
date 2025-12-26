namespace IK.SCP.Application.ENV.ViewModels
{
    public class ArranqueMaquinaViewModel
    {
        public int ArranqueMaquinaId { get; set; }
        public decimal? PesoSobreProducto1 { get; set; }
        public decimal? PesoSobreProducto2 { get; set; }
        public decimal? PesoSobreProducto3 { get; set; }
        public decimal? PesoSobreProducto4 { get; set; }
        public decimal? PesoSobreProducto5 { get; set; }
        public decimal? PesoSobreProductoProm { get; set; }
        public decimal? PesoSobreVacio { get; set; }
        //public string Observacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public List<ArranqueMaquinaObservacionViewModel> Observaciones { get; set; }
        public List<ArranqueMaquinaCondPrevCabViewModel> Condiciones { get; set; }
        public List<ArranqueMaquinaVarBasCabViewModel> Variables { get; set; }

    }

    public class ArranqueMaquinaObservacionViewModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }

    public class ArranqueMaquinaCondPrevCabViewModel
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string TipoDesc { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<ArranqueMaquinaCondPrevViewModel> detalles { get; set; }
    }

    public class ArranqueMaquinaVarBasCabViewModel
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Cerrado { get; set; }
        public List<ArranqueMaquinaVarBasViewModel> detalles { get; set; }
    }

    public class ArranqueMaquinaCondPrevViewModel
    {
        public int Id { get; set; }
        public int CondicionPreviaId { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public int Orden { get; set; }
        public bool Valor { get; set; }
        public string? Observacion { get; set; }
    }

    public class ArranqueMaquinaVarBasViewModel
    {
        public int id { get; set; }
        public int variableBasicaId { get; set; }
        public string padre { get; set; }
        public string nombre { get; set; }
        public string comentario { get; set; }
        public int primerOrden { get; set; }
        public int segundoOrden { get; set; }
        public string valor { get; set; }
        public string? observacion { get; set; }
        public bool cerrado { get; set; }
    }
}
