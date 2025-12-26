namespace IK.SCP.Domain.Dtos
{
    public class RegistroCaracterizacionCreateDto
    {
        public string OrdenId { get; set; }
        public string Etapa { get; set; }
        public decimal Peso { get; set; }
        public string Inspector { get; set; }
        public string? Observacion { get; set; }
        public List<RegistroCaracterizacionDetgalleCreateDto>? Defectos { get; set; }
    }

    public class RegistroCaracterizacionDetgalleCreateDto
    {
        public int Id { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Porcentaje { get; set; }
    }
}
