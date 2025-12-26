namespace IK.SCP.Application.FR.ViewModels
{
    public class InsertArranqueMaquinaVerificacionEquipoRequest
    {
        public int ArranqueMaquinaVerificacionEquipoCabId { get; set; }
        public int ArranqueMaquinaId { get; set; }
        public List<InsertVerificacionEquipoRequest> Verificaciones { get; set; }
    }

    public class InsertVerificacionEquipoRequest
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string? Operativo { get; set; }
        public string? Limpio { get; set; }
        public string? Observacion { get; set; }

    }
}
