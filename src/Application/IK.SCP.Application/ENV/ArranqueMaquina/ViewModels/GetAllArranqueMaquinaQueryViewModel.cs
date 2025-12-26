namespace IK.SCP.Application.ENV.ViewModels
{
    public class GetAllArranqueMaquinaQueryRequest
    {
        public int EnvasadoraId { get; set; }
        public string OrdenId { get; set; }
    }

    public class GetAllArranqueMaquinaQueryResponse
    {
        public int ArranqueMaquinaId { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Cerrado { get; set; }
    }
}
