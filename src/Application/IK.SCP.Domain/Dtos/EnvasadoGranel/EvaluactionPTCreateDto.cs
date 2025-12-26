namespace IK.SCP.Domain.Dtos
{
    public class EvaluactionPTCreateDto
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public Int16 Olor { get; set; }
        public Int16 Color { get; set; }
        public Int16 Sabor { get; set; }
        public Int16 Textura { get; set; }
        public Int16 AparienciaGeneral { get; set; }
        public Int16 CalificacionFinal { get; set; }
        public string Panelistas { get; set; }
        public string Observacion { get; set; }

    }
}
