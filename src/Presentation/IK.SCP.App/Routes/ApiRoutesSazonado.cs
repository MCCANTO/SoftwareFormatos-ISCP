namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        public const string GET_ALL_SAZONADOR = URL_BASE + "/sazonado/linea";

        public const string GET_ALL_SAZONADO_LINEA_FR = URL_BASE + "/sazonado/freidora";

        public const string GET_ALL_SAZONADO_ORDEN_FR = URL_BASE + "/sazonado/orden";

        public const string GET_ALL_SAZONADO_SABOR_FR = URL_BASE + "/sazonado/sabor";

        public const string POST_SAZONADO_ARRANQUE = URL_BASE + "/sazonado/arranque";

        public const string POST_SAZONADO_ARRANQUE_CONDICION = URL_BASE + "/sazonado/arranque/condicion";
        public const string POST_SAZONADO_ARRANQUE_OBSERVACION = URL_BASE + "/sazonado/arranque/observacion";

        public const string GET_SAZONADO_ARRANQUE_VERIFICACION_EQUIPO = URL_BASE + "/sazonado/arranque/verificacion-equipo-detalle/{Id}";
        public const string POST_SAZONADO_ARRANQUE_VERIFICACION_EQUIPO = URL_BASE + "/sazonado/arranque/verificacion-equipo-detalle";

        public const string POST_SAZONADO_ARRANQUE_VARIABLE_BASICA = URL_BASE + "/sazonado/arranque/variable-basica";
        
        public const string PRINT_SAZONADO_CHECKLIST_ARRANQUE = URL_BASE + "/sazonado/arranque/PDF";

    }
}
