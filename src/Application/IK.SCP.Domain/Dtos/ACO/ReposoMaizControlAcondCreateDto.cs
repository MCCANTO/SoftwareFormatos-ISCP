namespace IK.SCP.Domain.Dtos
{
    public class ReposoMaizControlAcondCreateDto
    {
        public int? Id { get; set; }
        public string OrdenId { get; set; }
        public int NumeroBatch { get; set; }
        public decimal CantidadBatch { get; set; }
        public string? FechaHoraInicioReposo { get; set; }
        public string? FechaHoraInicioFritura { get; set; }
        public string? Observacion { get; set; }

    }

    public class RemojoHabaControlAcondCreateDto
    {
        public int? Id { get; set; }
        public string OrdenId { get; set; }
        public int NumeroBatch { get; set; }
        public decimal CantidadBatch { get; set; }
        public string? FechaHoraInicioReposo { get; set; }
        public string? FechaHoraInicioFritura { get; set; }
        public string? Observacion { get; set; }

    }
}
