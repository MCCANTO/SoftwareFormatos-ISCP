using System;
namespace IK.SCP.Application.ENV.ViewModels
{
	public class VariableBasicaViewModel
	{
        public int Id { get; set; }
        public int VariableBasicaId { get; set; }
        public string Padre { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public string valor { get; set; }
        public int PrimerOrden { get; set; }
        public int SegundoOrden { get; set; }
    }
}

