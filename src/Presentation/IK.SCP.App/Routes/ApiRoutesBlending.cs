namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        public const string GET_ALL_BLENDING_ARTICULO_MEZCLA = URL_BASE + "/blending/validacion";
        public const string GET_ALL_BLENDING_COMPONENTES = URL_BASE + "/blending/componentes";
        public const string GET_ALL_BLENDING_ARRANQUES = URL_BASE + "/blending/arranque";
        public const string POST_BLENDING_ARRANQUE = URL_BASE + "/blending/arranque";
        public const string PUT_BLENDING_ARRANQUE_CIERRE = URL_BASE + "/blending/arranque/cierre";
        public const string GET_BLENDING_ARRANQUE_OPEN = URL_BASE + "/blending/arranque/activo";
        public const string GET_BLENDING_ARRANQUE_X_ID = URL_BASE + "/blending/arranque/{Id}";

        public const string GET_BLENDING_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/blending/verificacion-equipo-detalle/{Id}";
        public const string POST_BLENDING_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/blending/verificacion-equipo-detalle";
        public const string PUT_BLENDING_CONDICION = URL_BASE + "/blending/condicion-previa";
        public const string POST_BLENDING_OBSERVACION = URL_BASE + "/blending/observacion";

        public const string GET_BLENDING_CONTROL_COMPONENTES = URL_BASE + "/blending/control-componentes";
        public const string POST_BLENDING_CONTROL_COMPONENTES = URL_BASE + "/blending/control-componentes";
        public const string PUT_BLENDING_CONTROL_COMPONENTES = URL_BASE + "/blending/control-componentes";
        public const string GET_BLENDING_CONTROL = URL_BASE + "/blending/control";
        public const string POST_BLENDING_CONTROL = URL_BASE + "/blending/control";
        public const string GET_BLENDING_CONTROL_MERMA = URL_BASE + "/blending/control-merma";
        
        
        public const string PRINT_BLENDING_ARRANQUE = URL_BASE + "/blending/arranque/PDF";
        public const string PRINT_BLENDING_CONTROL = URL_BASE + "/blending/control/PDF";
    }
}
