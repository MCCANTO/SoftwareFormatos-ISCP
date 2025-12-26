using System;
namespace IK.SCP.Application.FR.ViewModels
{
	public class VerificacionEquipoViewModel
	{
        public int VerificacionEquipoId { get; set; }
        public string Nombre { get; set; }
        public string? Comentario { get; set; }
        public int Orden { get; set; }
        public int? PadreId { get; set; }
    }



    public class VerificacionEquipoResponse
    {
        public string Categoria { get; set; }
        public List<VerificacionEquipoDetalle> Verificaciones { get; set; }
    }

    public class VerificacionEquipoDetalle
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public string Operativo { get; set; }
        public string Limpio { get; set; }
        public string Observacion { get; set; }
        public bool Cerrado { get; set; }
        public int Orden { get; set; }
    }
}

