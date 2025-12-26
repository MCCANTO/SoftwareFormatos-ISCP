using System;
namespace IK.SCP.Domain.Dtos
{
	public class EspecificacionGranelDto
	{
		public int Id { get; set; }
		public int EspecificacionId { get; set; }
		public string Nombre { get; set; }
		public int? Valor { get; set; }
		public string Otro { get; set; }
		public List<EspecificacionGranelDetalleDto>  Valores { get; set; }
	}

	public class EspecificacionGranelDetalleDto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
	}
}

