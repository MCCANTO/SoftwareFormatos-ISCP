namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        public const string GET_ALL_FREIDORA = URL_BASE + "/fritura/linea";
        public const string GET_BY_ID_ORDEN_FR = URL_BASE + "/fritura/orden";
        public const string GET_ALL_ORDEN_FR = URL_BASE + "/fritura/ordenes";
        public const string GET_ALL_ORDEN_FR_CONSUMO = URL_BASE + "/fritura/orden/consumo";

        public const string GET_ALL_FR_CONDICION_PREVIA = URL_BASE + "/fritura/arranquemaquina/condicionprevia";

        public const string GET_ALL_FR_PANELISTA = URL_BASE + "/fritura/panelista";

        public const string GET_FR_ARRANQUE_MAQUINA_ABIERTO = URL_BASE + "/fritura/arranquemaquina/activo";
        public const string GET_ALL_FR_ARRANQUE_MAQUINA = URL_BASE + "/fritura/arranquemaquina";
        public const string INSERT_FR_ARRANQUE_MAQUINA = URL_BASE + "/fritura/arranquemaquina";
        public const string UPDATE_FR_ARRANQUE_MAQUINA = URL_BASE + "/fritura/arranquemaquina";

        public const string GET_ALL_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO = URL_BASE + "/fritura/arranquemaquina/verificacionequipo";
        public const string GET_ALL_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/fritura/arranquemaquina/verificacionequipo/detalle";
        public const string INSERT_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO = URL_BASE + "/fritura/arranquemaquina/verificacionequipo";
    
        public const string GET_ALL_FR_EVALUACION_ATRIBUTO = URL_BASE + "/fritura/evaluacionatributo";
        public const string GET_BY_ID_FR_EVALUACION_ATRIBUTO = URL_BASE + "/fritura/evaluacionatributo/{Id}";
        public const string INSERT_FR_EVALUACION_ATRIBUTO = URL_BASE + "/fritura/evaluacionatributo";
        
        public const string INSERT_FR_ARRANQUE_MAQUINA_OBSERVACION = URL_BASE + "/fritura/arranquemaquina/observacion";


        public const string GET_ALL_FR_CONTROL_ACEITE = URL_BASE + "/fritura/control-aceite";
        public const string POST_FR_CONTROL_ACEITE = URL_BASE + "/fritura/control-aceite";


        public const string GET_ALL_FR_DEFECTO_CARACTERIZACION = URL_BASE + "/fritura/defecto";
        public const string GET_ALL_FR_REGISTRO_CARACTERIZACION = URL_BASE + "/fritura/caracterizacion";
        public const string POST_FR_REGISTRO_CARACTERIZACION = URL_BASE + "/fritura/caracterizacion";
        
        
        public const string GET_FR_ATTRIBUTE_EVALUATION_PDF = URL_BASE + "/fritura/evaluacionatributo/PDF";
        public const string GET_FR_ARRANQUE_MAQUINA_PDF = URL_BASE + "/fritura/arranquemaquina/PDF/{Orden}/{Linea}/{IdArranqueMaquina}/{Articulo}";
        public const string PRINT_FR_CONTROL_ACEITE = URL_BASE + "/fritura/control-aceite/pdf";
        public const string PRINT_FR_CARACTERIZACION_PRODUCTO_TERMINADO = URL_BASE + "/fritura/caracterizacion/pdf";
    }
}
