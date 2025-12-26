namespace IK.SCP.Application.FR.ViewModels
{
    public class GetAllEvaluacionAtributoResponse
    {
        public int EvaluacionAtributoId { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CalificacionFinal { get; set; }
        public string Panelistas { get; set; }
        public string Resultado { get; set; }
    }
}
