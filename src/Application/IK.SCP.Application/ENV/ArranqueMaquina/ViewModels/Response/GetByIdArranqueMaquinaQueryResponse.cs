namespace IK.SCP.Application.ENV.ArranqueMaquina.ViewModels.Response
{
    public class GetByIdArranqueMaquinaQueryResponse
    {
        public int ArranqueMaquinaId { get; set; }
        public decimal PesoSobreProducto1 { get; set; }
        public decimal PesoSobreProducto2 { get; set; }
        public decimal PesoSobreProducto3 { get; set; }
        public decimal PesoSobreProducto4 { get; set; }
        public decimal PesoSobreProducto5 { get; set; }
        public decimal PesoSobreProductoProm { get; set; }
        public decimal PesoSobreVacio { get; set; }
        public decimal Observacion { get; set; }
        public decimal Cerrado { get; set; }
        public decimal UsuarioCreacion { get; set; }
        public List<GetByIdArranqueMaquinaCondicionesResponse> condiciones { get; set; }
        public List<GetByIdArranqueMaquinaVariableBasicaResponse> variables { get; set; }
    }

    public class GetByIdArranqueMaquinaCondicionesResponse
    {
        public int ArranqueMaquinaCondPrevCabId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class GetByIdArranqueMaquinaVariableBasicaResponse
    {
        public int ArranqueMaquinaVarBasCabId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
