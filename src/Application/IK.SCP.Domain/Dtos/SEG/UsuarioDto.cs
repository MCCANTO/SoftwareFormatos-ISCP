namespace IK.SCP.Domain.Dtos
{
    public class UsuarioDto
    {
        public string UsuarioId { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Nombres { get; set; }

        public string Correo { get; set; }

        public List<PerfilDto> Perfiles { get; set; }
        public List<OpcionDto> Opciones { get; set; }

        public UsuarioDto()
        {
            Perfiles = new List<PerfilDto>();
            Opciones = new List<OpcionDto>();
        }
    }
}
