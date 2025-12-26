using System;
namespace IK.SCP.Application.FR.ViewModels
{
	public class CondicionPreviaViewModel
	{
		public int CondicionPreviaId { get; set; }
		public string Nombre { get; set; }
		public string Comentario { get; set; }
		public int Orden { get; set; }
        public bool Valor { get; set; }
    }
}

