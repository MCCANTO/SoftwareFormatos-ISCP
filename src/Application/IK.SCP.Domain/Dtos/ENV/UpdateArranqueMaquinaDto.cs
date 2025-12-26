using System;
namespace IK.SCP.Domain.Dtos
{
	public class UpdateArranqueMaquinaDto
	{
        public ArranqueMaquinaDto principal { get; set; }
        public List<UpdateCondicionPreviaDto>? condicionesPrevias { get; set; } = null;
        public List<UpdateVariableBasicaDto>? variablesBasicas { get; set; } = null;
    }

    public class UpdateCondicionPreviaDto
    {
        public int id { get; set; }
        public int tipoId { get; set; }
        public List<UpdateCondicionPreviaDetalleDto>? detalles { get; set; } = null;
    }

    public class UpdateCondicionPreviaDetalleDto
    {
        public int id { get; set; }
        public int condicionPreviaId { get; set; }
        public bool valor { get; set; }
        public string? observacion { get; set; }
    }


    public class UpdateVariableBasicaDto
    {
        public int id { get; set; }
        public List<UpdateVariableBasicaDetalleDto>? detalles { get; set; } = null;
    }

    public class UpdateVariableBasicaDetalleDto
    {
        public int id { get; set; }
        public int variableBasicaId { get; set; }
        public string valor { get; set; }
        public string?observacion { get; set; }
    }
}

