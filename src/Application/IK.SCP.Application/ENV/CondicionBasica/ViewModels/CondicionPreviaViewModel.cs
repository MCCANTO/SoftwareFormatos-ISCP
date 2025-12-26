using System;
namespace IK.SCP.Application.ENV.ViewModels
{
	public class CondicionPreviaViewModel
	{
		public int Id { get; set; }
        public int CondicionPreviaId { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public int Orden { get; set; }
	}
}

