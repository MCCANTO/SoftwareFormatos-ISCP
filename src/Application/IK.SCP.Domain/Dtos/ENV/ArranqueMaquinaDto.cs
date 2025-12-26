using System;
namespace IK.SCP.Domain.Dtos
{
	public class ArranqueMaquinaDto
	{
        public int arranqueMaquinaId { get; set; }
        public int envasadoraId { get; set; }
        public string? ordenId { get; set; }
        public decimal? pesoSobreProducto1 { get; set; }
        public decimal? pesoSobreProducto2 { get; set; }
        public decimal? pesoSobreProducto3 { get; set; }
        public decimal? pesoSobreProducto4 { get; set; }
        public decimal? pesoSobreProducto5 { get; set; }
        public decimal? pesoSobreProductoProm { get; set; }
        public decimal? pesoSobreVacio { get; set; }
        public string? observacion { get; set; }
        public bool cerrado { get; set; }

    }

    public class ArranqueMaquinaCondicionPreviaDto
    {
        public int id { get; set; }
        public int tipoId { get; set; }
        public List<ArranqueMaquinaCondicionPreviaItemDto>? items { get; set; }
    }

    public class ArranqueMaquinaCondicionPreviaItemDto
    {
        public int? id { get; set; }
        public int condicionPreviaId { get; set; }
        public string? nombre { get; set; }
        public string? comentario { get; set; }
        public bool valor { get; set; }
        public string? observacion { get; set; }
        public int orden { get; set; }
    }


    public class ArranqueMaquinaVariableBasicaDto
    {
        public int? id { get; set; }
        public List<ArranqueMaquinaVariableBasicaItemDto>? items { get; set; }
    }



    public class ArranqueMaquinaVariableBasicaItemDto
    {
        public string? padre { get; set; }
        public List<ArranqueMaquinaVariableBasicaSubItemDto>? items { get; set; }
    }


    public class ArranqueMaquinaVariableBasicaSubItemDto
    {
        public int? id { get; set; }
        public int? variableBasicaId { get; set; }
        public string? nombre { get; set; }
        public string? comentario { get; set; }
        public string? valor { get; set; }
        public string? observacion { get; set; }
        public bool cerrado { get; set; }
        public int? primerOrden { get; set; }
        public int? segundoOrden { get; set; }
    }

    public class ArranqueEnvasadoDto
    {
        public int arranqueId { get; set; }
        public int envasadoraId { get; set; }
        public int ordenId { get; set; }
        public bool cerrado { get; set; }
    }
}

