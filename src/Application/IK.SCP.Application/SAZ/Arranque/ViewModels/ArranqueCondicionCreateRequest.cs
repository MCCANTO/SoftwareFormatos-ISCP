namespace IK.SCP.Application.SAZ.ViewModels
{
    public class ArranqueCondicionCreateRequest
    {
        public List<ArranqueCondicionPreviaRequest> condiciones { get; set; }
    }

    public class ArranqueCondicionPreviaRequest
    {
        public int ArranqueCondicionPreviaId { get; set; }
        public bool Valor { get; set; }
    }
}
