namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        public const string GET_ALL_ACOND_MATERIA_PRIMA = URL_BASE + "/acondicionamiento/materia-prima";
        public const string GET_ALL_ACOND_PROCESO_MATERIA_PRIMA = URL_BASE + "/acondicionamiento/proceso-materia-prima";
        public const string GET_ALL_ACOND_ORDEN = URL_BASE + "/acondicionamiento/orden";
        public const string GET_FR_MAIZ_MOJADO_REMOJO_HABAS = URL_BASE + "/acondicionamiento/orden/pdf/reposo-maiz";
        
        
        public const string GET_BY_ID_ACOND_ORDEN = URL_BASE + "/acondicionamiento/orden/{Id}";
        public const string POST_ACOND_ORDEN = URL_BASE + "/acondicionamiento/orden";
        public const string PUT_ACOND_ORDEN_CIERRE = URL_BASE + "/acondicionamiento/orden/{OrdenId}";

        public const string GET_ALL_ACOND_ARRANQUE_MAIZ = URL_BASE + "/acondicionamiento/arranque-maiz";
        public const string GET_BY_ID_ACOND_ARRANQUE_MAIZ = URL_BASE + "/acondicionamiento/arranque-maiz/detalle/{ArranqueMaizId}";
        public const string GET_ACOND_ARRANQUE_MAIZ_ACTIVO = URL_BASE + "/acondicionamiento/arranque-maiz/{OrdenId}";
        public const string POST_ACOND_ARRANQUE_MAIZ = URL_BASE + "/acondicionamiento/arranque-maiz";
        public const string PUT_ACOND_ARRANQUE_MAIZ_CIERRE = URL_BASE + "/acondicionamiento/arranque-maiz/cierre/{ArranqueMaizId}";

        public const string PUT_ACOND_ARRANQUE_MAIZ_CONDICION = URL_BASE + "/acondicionamiento/arranque-maiz/condicion-previa";
        public const string GET_ACOND_ARRANQUE_MAIZ_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/arranque-maiz/verificacion-equipo/{Id}";
        public const string PUT_ACOND_ARRANQUE_MAIZ_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/arranque-maiz/verificacion-equipo";
        public const string PUT_ACOND_ARRANQUE_MAIZ_VARIABLE_BASICA = URL_BASE + "/acondicionamiento/arranque-maiz/variable-basica";
        public const string POST_ACOND_ARRANQUE_MAIZ_OBSERVACION = URL_BASE + "/acondicionamiento/arranque-maiz/observacion";


        public const string GET_ACOND_CONTROL_MAIZ_MATERIA_PRIMA = URL_BASE + "/acondicionamiento/control-maiz/materia-prima";
        public const string POST_ACOND_CONTROL_MAIZ_MATERIA_PRIMA = URL_BASE + "/acondicionamiento/control-maiz/materia-prima";

        public const string GET_ACOND_CONTROL_MAIZ_INSUMO = URL_BASE + "/acondicionamiento/control-maiz/insumo";
        public const string POST_ACOND_CONTROL_MAIZ_INSUMO = URL_BASE + "/acondicionamiento/control-maiz/insumo";

        public const string GET_ACOND_CONTROL_MAIZ_OBSERVACION = URL_BASE + "/acondicionamiento/control-maiz/observacion";
        public const string POST_ACOND_CONTROL_MAIZ_OBSERVACION = URL_BASE + "/acondicionamiento/control-maiz/observacion";

        public const string GET_ACOND_CONTROL_MAIZ_PELADO = URL_BASE + "/acondicionamiento/control-maiz/pelado";
        public const string POST_ACOND_CONTROL_MAIZ_PELADO = URL_BASE + "/acondicionamiento/control-maiz/pelado";

        public const string GET_ACOND_CONTROL_MAIZ_REMOJO = URL_BASE + "/acondicionamiento/control-maiz/remojo";
        public const string POST_ACOND_CONTROL_MAIZ_REMOJO = URL_BASE + "/acondicionamiento/control-maiz/remojo";

        public const string GET_ACOND_CONTROL_MAIZ_SANCOCHADO = URL_BASE + "/acondicionamiento/control-maiz/sancochado";
        public const string POST_ACOND_CONTROL_MAIZ_SANCOCHADO = URL_BASE + "/acondicionamiento/control-maiz/sancochado";


        public const string GET_ACOND_CONTROL_MAIZ_REPOSO = URL_BASE + "/acondicionamiento/control-maiz/reposo";
        public const string GET_ACOND_CONTROL_MAIZ_SANCOCHADO_BATCH = URL_BASE + "/acondicionamiento/control-maiz/reposo/sancochado";
        public const string POST_ACOND_CONTROL_MAIZ_REPOSO = URL_BASE + "/acondicionamiento/control-maiz/reposo";

        public const string GET_ACOND_CONTROL_HABA_REMOJO = URL_BASE + "/acondicionamiento/control-haba/remojo";
        public const string POST_ACOND_CONTROL_HABA_REMOJO = URL_BASE + "/acondicionamiento/control-haba/remojo";

        public const string GET_ALL_ACOND_ARRANQUE_LAVADO_TUBERCULO = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo";
        public const string GET_BY_ID_ACOND_ARRANQUE_LAVADO_TUBERCULO = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/detalle/{ArranqueLavadoTuberculoId}";
        public const string GET_ACOND_ARRANQUE_LAVADO_TUBERCULO_ACTIVO = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/{OrdenId}";
        public const string POST_ACOND_ARRANQUE_LAVADO_TUBERCULO = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo";
        public const string PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_CIERRE = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/cierre";

        public const string PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_CONDICION = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/condicion-previa";
        public const string GET_ACOND_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/verificacion-equipo/{Id}";
        public const string PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/verificacion-equipo";

        public const string GETALL_ACOND_CONTROL_RAYOS_X = URL_BASE + "/acondicionamiento/control-rayos-x";
        public const string POST_ACOND_CONTROL_RAYOS_X = URL_BASE + "/acondicionamiento/control-rayos-x";
        public const string PUT_ACOND_CONTROL_RAYOS_X_REVISION = URL_BASE + "/acondicionamiento/control-rayos-x";



        public const string GETALL_ACOND_CHECKLIST_ELECTROPORADOR = URL_BASE + "/acondicionamiento/checklist-pef";
        public const string GET_BY_ID_ACOND_CHECKLIST_ELECTROPORADOR = URL_BASE + "/acondicionamiento/checklist-pef/{Id}";
        public const string GET_ACOND_CHECKLIST_ELECTROPORADOR_ACTIVO = URL_BASE + "/acondicionamiento/checklist-pef/activo/{OrdenId}";
        public const string POST_ACOND_CHECKLIST_ELECTROPORADOR = URL_BASE + "/acondicionamiento/checklist-pef";
        public const string PUT_ACOND_CHECKLIST_ELECTROPORADOR = URL_BASE + "/acondicionamiento/checklist-pef/cierre";
        public const string PUT_ACOND_CHECKLIST_ELECTROPORADOR_CONDICION = URL_BASE + "/acondicionamiento/checklist-pef/condicion-previa";
        public const string GET_ACOND_CHECKLIST_ELECTROPORADOR_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/checklist-pef/verificacion-equipo/{Id}";
        public const string PUT_ACOND_CHECKLIST_ELECTROPORADOR_VERIFICACION_EQUIPO_DETALLE = URL_BASE + "/acondicionamiento/checklist-pef/verificacion-equipo";
        public const string PUT_ACOND_CHECKLIST_ELECTROPORADOR_VARIABLE_BASICA = URL_BASE + "/acondicionamiento/checklist-pef/variable-basica";
        
        
        public const string GET_ACOND_CONTROL_TRATAMIENTO_PEF = URL_BASE + "/acondicionamiento/control-pef";
        //public const string GET_BY_ID_ACOND_CONTROL_TRATAMIENTO_PEF = URL_BASE + "/acondicionamiento/control-pef/{Id}";
        public const string POST_ACOND_CONTROL_TRATAMIENTO_PEF = URL_BASE + "/acondicionamiento/control-pef";
        public const string PUT_ACOND_CONTROL_TRATAMIENTO_PEF = URL_BASE + "/acondicionamiento/control-pef";
        //public const string PUT_ACOND_CONTROL_TRATAMIENTO_PEF_CIERRE = URL_BASE + "/acondicionamiento/control-pef/cierre";
        public const string GET_ACOND_CONTROL_TRATAMIENTO_PEF_CONDICION_PREVIA_DETALLE = URL_BASE + "/acondicionamiento/control-pef/condicion-previa/{Id}";
        public const string POST_ACOND_CONTROL_TRATAMIENTO_PEF_CONDICION_PREVIA = URL_BASE + "/acondicionamiento/control-pef/condicion-previa";
        public const string GET_ACOND_CONTROL_TRATAMIENTO_PEF_FUERZA_CORTE_DETALLE = URL_BASE + "/acondicionamiento/control-pef/fuerza-corte/{Id}";
        public const string POST_ACOND_CONTROL_TRATAMIENTO_PEF_FUERZA_CORTE = URL_BASE + "/acondicionamiento/control-pef/fuerza-corte";
        public const string POST_ACOND_CONTROL_TRATAMIENTO_PEF_TIEMPO = URL_BASE + "/acondicionamiento/control-pef/tiempo";
       
        
        public const string PRINT_ACOND_CONTROL_MAIZ_REPOSO = URL_BASE + "/acondicionamiento/control-maiz/reposo/pdf";
        public const string PRINT_ACOND_CONTROL_HABA_REMOJO = URL_BASE + "/acondicionamiento/control-haba/remojo/pdf";
        public const string PRINT_ACOND_CONTROL_MAIZ_PELADO_REMOJO_SANCOCHADO = URL_BASE + "/acondicionamiento/control-maiz/pdf";
        public const string PRINT_ACOND_CONTROL_TRATAMIENTO_PEF = URL_BASE + "/acondicionamiento/control-pef/pdf";
        public const string PRINT_ACOND_CHECKLIST_ELECTROPORADOR = URL_BASE + "/acondicionamiento/checklist-pef/pdf";
        public const string PRINT_ACOND_CHECKLIST_LAVADO_TUBERCULO = URL_BASE + "/acondicionamiento/arranque-lavado-tuberculo/pdf";
        public const string PRINT_ACOND_CONTROL_RAYOS_X = URL_BASE + "/acondicionamiento/control-rayos-x/pdf";
        public const string PRINT_ACOND_ARRANQUE_MAIZ = URL_BASE + "/acondicionamiento/arranque-maiz/pdf";
    }
}
