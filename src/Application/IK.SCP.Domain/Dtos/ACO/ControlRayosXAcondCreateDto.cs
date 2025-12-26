using System;
namespace IK.SCP.Domain.Dtos
{
	public class ControlRayosXAcondCreateDto
	{
		public int MateriaPrima { get; set; }
		public bool DeteccionUno { get; set; }
		public bool DeteccionDos { get; set; }
		public string Conformidad { get; set; }
		public string? Observacion { get; set; }
	}
}

