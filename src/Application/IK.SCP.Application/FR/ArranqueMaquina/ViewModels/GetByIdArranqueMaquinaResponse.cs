using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace IK.SCP.Application.FR.ViewModels
{
    public class GetByIdArranqueMaquinaResponse
    {
        public int ArranqueMaquinaId { get; set; }
        public bool Cerrado { get; set; }
        public bool Revisado { get; set; }

        public List<GetByIdArranqueMaquinaCondicionResponse> Condiciones { get; set; }
        public List<GetByIdArranqueMaquinaVerificacionEquipoResponse> Verificaciones { get; set; }
        public List<GetByIdArranqueMaquinaObservacionResponse> Observaciones { get; set; }
    }

    public class GetByIdArranqueMaquinaCondicionResponse
    {
        public int ArranqueMaquinaCondicionPreviaId { get; set; }
        public int CondicionPreviaId { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public bool Valor { get; set; }
        public int Orden { get; set; }
    }

    public class GetByIdArranqueMaquinaVerificacionEquipoResponse
    {
        public int ArranqueMaquinaVerificacionEquipoCabId { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrado { get; set; }
    }

    public class GetByIdArranqueMaquinaObservacionResponse
    {
        public int ArranqueMaquinaObservacionId { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
    
    public class GetByIdArranqueManufacturaResponse
    {
        public int ArranqueMaquinaId { get; set; }
        public bool Cerrado { get; set; }
        public bool Revisado { get; set; }

        public List<GetByIdArranqueMaquinaCondicionResponse> Condiciones { get; set; }
        public GetByIdMaquinaVerificacionEquipoResponse Verificaciones { get; set; }
        public List<GetByIdArranqueMaquinaObservacionResponse> Observaciones { get; set; }
        public List<GetByIdArranqueMaquinaEvaluacionSensorial> Sensorials { get; set; }
    }

    public class GetByIdMaquinaVerificacionEquipoResponse
    {
        public int ArranqueMaquinaVerificacionEquipoCabId { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrado { get; set; }
        public List<GetByIdMaquinaVerificacionEquipoArranqueResponse> VerificacionesArranque { get; set; }
    }

    public class GetByIdMaquinaVerificacionEquipoArranqueResponse
    {
        public int Id { get; set; }
        public int VerificacionEquipoId { get; set; }
        public int Orden_1 { get; set; }
        public string Nombre_1 { get; set; }
        public string Comentario_1 { get; set; }
        public int Orden_2 { get; set; }
        public string Nombre_2 { get; set; }
        public string Comentario_2 { get; set; }
        public int Orden_3 { get; set; }
        public string Nombre_3 { get; set; }
        public string Comentario_3 { get; set; }
        public string Operativo { get; set; }
        public string Limpio { get; set; }
        public int Cerrado { get; set; }
        public string Observacion { get; set; }
        public int rowSpan { get; set; }
    }

    public class GetByIdArranqueMaquinaEvaluacionSensorial
    {
        public int AparienciaGeneral { get; set; }
        public int Color { get; set; }
        public int Olor { get; set; }
        public int Sabor { get; set; }
        public int Textura { get; set; }
        public int CalificacionFinal { get; set; }
        public string Panelistas { get; set; }
        public string Observacion { get; set; }
    }
}
