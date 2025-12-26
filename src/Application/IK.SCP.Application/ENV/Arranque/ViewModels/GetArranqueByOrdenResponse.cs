namespace IK.SCP.Application.ENV.ViewModels
{
    public class GetArranqueByOrdenResponse
    {
        public int ArranqueId { get; set; }
        public bool Cerrado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
    
    public class GetArranqueEnvasadoResponse
    {
        public int ArranqueId { get; set; }
        public bool Cerrado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
    }
}
