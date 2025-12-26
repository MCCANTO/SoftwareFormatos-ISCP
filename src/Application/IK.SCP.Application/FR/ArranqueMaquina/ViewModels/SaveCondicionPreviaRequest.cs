using System;
namespace IK.SCP.Application.FR.ViewModels
{
	public class SaveCondicionPreviaRequest
	{
		public int ArranqueMaquinaId { get; set; }
		public List<CondicionesRequest> Condiciones { get; set; }
	}

	public class CondicionesRequest
	{
		public int ArranqueMaquinaCondicionPreviaId { get; set; }
        public int CondicionPreviaId { get; set; }
        public bool Valor { get; set; }
        public string Observacion { get; set; }
    }
}

