namespace IK.SCP.Domain.Dtos
{
    public class OpcionDto
    {
        public int OpcionId { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public int? Orden { get; set; }

        public string Icono { get; set; }

        public string Color { get; set; }

        public int PadreId { get; set; }
    }
}
