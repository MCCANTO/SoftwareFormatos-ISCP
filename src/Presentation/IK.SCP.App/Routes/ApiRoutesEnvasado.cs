namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        public const string GET_ALL_ENVASADORA = URL_BASE + "/envasado/envasadora";
        public const string GET_BY_ID_ORDEN = URL_BASE + "/envasado/orden/{Orden}";

        public const string GET_ARRANQUE_MAQUINA_ABIERTO = URL_BASE + "/envasado/arranquemaquina/abierto";
        public const string GET_ALL_ARRANQUE_MAQUINA = URL_BASE + "/envasado/arranquemaquina";
        public const string GET_BY_ID_ARRANQUE_MAQUINA = URL_BASE + "/envasado/arranquemaquina/{id}";
        
        public const string GET_ARRANQUE_MAQUINA_COND_PREV = URL_BASE + "/envasado/arranquemaquina/condicionprevia";
        public const string GET_ARRANQUE_MAQUINA_VAR_BAS = URL_BASE + "/envasado/arranquemaquina/variablebasica";
        public const string GET_ARRANQUE_MAQUINA_OBS = URL_BASE + "/envasado/arranquemaquina/observacion";

        public const string GET_ARRANQUE_BY_ORDEN = URL_BASE + "/envasado/arranque";
        public const string GET_ALL_ARRANQUE_BY_ORDEN = URL_BASE + "/envasado/arranque/all";
        public const string POST_ARRANQUE = URL_BASE + "/envasado/arranque/activo";
        public const string POST_ARRANQUE_CARGA_ARCHIVO = URL_BASE + "/envasado/arranque/carga";
        public const string POST_ARRANQUE_CARGA_INSPECCION = URL_BASE + "/envasado/arranque/carga-inspeccion";

        public const string GET_ALL_CONDICION_PREVIA = URL_BASE + "/envasado/condicionprevia";
        public const string GET_ALL_VARIABLE_BASICA = URL_BASE + "/envasado/variablebasica";

        public const string GET_ALL_ARRANQUE_VARIABLE_BASICA = URL_BASE + "/envasado/arranque/variablebasica";
        public const string GET_ARRANQUE_VARIABLE_BASICA = URL_BASE + "/envasado/arranque/variablebasica/{ArranqueVariableBasicaCabId}";
        public const string POST_ARRANQUE_VARIABLE_BASICA = URL_BASE + "/envasado/arranque/variablebasica";

        public const string GET_ALL_ARRANQUE_CODIFICACION = URL_BASE + "/envasado/arranque/codificacion";
        public const string POST_ARRANQUE_CODIFICACION = URL_BASE + "/envasado/arranque/codificacion";

        public const string GET_ALL_ARRANQUE_CONDICIONPREVIA = URL_BASE + "/envasado/arranque/condicionprevia";
        public const string PUT_ARRANQUE_CONDICIONPREVIA = URL_BASE + "/envasado/arranque/condicionprevia";

        public const string GET_ALL_ARRANQUE_CONTRAMUESTRA = URL_BASE + "/envasado/arranque/contramuestra";
        public const string POST_ARRANQUE_CONTRAMUESTRA = URL_BASE + "/envasado/arranque/contramuestra";

        public const string GET_ALL_ARRANQUE_PERSONAL = URL_BASE + "/envasado/arranque/personal";
        public const string GET_ARRANQUE_PERSONAL = URL_BASE + "/envasado/arranque/persona";
        public const string POST_ARRANQUE_PERSONAL = URL_BASE + "/envasado/arranque/personal";

        public const string GET_ALL_ARRANQUE_COMPONENTE = URL_BASE + "/envasado/arranque/componente";
        public const string POST_ARRANQUE_COMPONENTE = URL_BASE + "/envasado/arranque/componente";

        public const string GET_ALL_ARRANQUE_OBSERVACION = URL_BASE + "/envasado/arranque/observacion";
        public const string POST_ARRANQUE_OBSERVACION = URL_BASE + "/envasado/arranque/observacion";

        public const string GET_ALL_ARRANQUE_INSPECCION = URL_BASE + "/envasado/arranque/inspeccion";
        public const string POST_ARRANQUE_INSPECCION = URL_BASE + "/envasado/arranque/inspeccion";

        public const string GET_ALL_ARRANQUE_REVISION = URL_BASE + "/envasado/arranque/revision";
        public const string POST_ARRANQUE_REVISION = URL_BASE + "/envasado/arranque/revision";

        public const string GET_ALL_PARAMETRO_GENERAL = URL_BASE + "/envasado/parametrogeneral";


        public const string GET_ALL_GRANEL_CHECKLIST = URL_BASE + "/envasado-granel/checklists";
        public const string POST_GRANEL_CHECKLIST_ORDEN = URL_BASE + "/envasado-granel/checklist";
        public const string POST_GRANEL_CHECKLIST_CIERRE = URL_BASE + "/envasado-granel/checklist/cierre/{ArranqueGranelId}";
        public const string POST_GRANEL_CHECKLIST_DATOS = URL_BASE + "/envasado-granel/checklist/datos";
        public const string POST_GRANEL_CHECKLIST_PERSONAL = URL_BASE + "/envasado-granel/checklist/personal";
        public const string GET_GRANEL_CHECKLIST_ORDEN = URL_BASE + "/envasado-granel/checklist";
        public const string GET_GRANEL_CHECKLIST = URL_BASE + "/envasado-granel/checklist/{Id}";

        public const string GET_GRANEL_CHECKLIST_REVISION = URL_BASE + "/envasado-granel/checklist/revision";
        public const string POST_GRANEL_CHECKLIST_REVISION = URL_BASE + "/envasado-granel/checklist/revision";

        public const string GET_GRANEL_CHECKLIST_ESPECIFICACIONES = URL_BASE + "/envasado-granel/checklist/especificacion";

        public const string GET_GRANEL_CONDICION_OPERATIVA_DETALLE = URL_BASE + "/envasado-granel/condicion-operativa/{Id}";
        public const string POST_GRANEL_CONDICION_OPERATIVA_DETALLE = URL_BASE + "/envasado-granel/condicion-operativa";
        public const string GET_GRANEL_CONDICION_PROCESO_DETALLE = URL_BASE + "/envasado-granel/condicion-proceso/{Id}";
        public const string PUT_GRANEL_CONDICION_PROCESO_DETALLE = URL_BASE + "/envasado-granel/condicion-proceso";

        public const string POST_GRANEL_OBSERVACION = URL_BASE + "/envasado-granel/observacion";



        public const string GET_ALL_GRANEL_PARAMETRO_CONTROL = URL_BASE + "/envasado-granel/parametros-control";
        public const string GET_ALL_GRANEL_CONTROL = URL_BASE + "/envasado-granel/control";
        public const string POST_GRANEL_CONTROL_OBSERVACION = URL_BASE + "/envasado-granel/control/observacion";



        public const string GET_ALL_GRANEL_EVALUACION_PT = URL_BASE + "/envasado-granel/evaluacion";
        public const string GET_GRANEL_EVALUACION_PT = URL_BASE + "/envasado-granel/evaluacion/{Id}";
        public const string POST_GRANEL_EVALUACION_PT = URL_BASE + "/envasado-granel/evaluacion";

        public const string POST_GRANEL_CODIFICACION_CARGA = URL_BASE + "/envasado-granel/codificacion/carga";
        public const string GET_ALL_GRANEL_CODIFICACION = URL_BASE + "/envasado-granel/codificacion";
        public const string POST_GRANEL_CODIFICACION = URL_BASE + "/envasado-granel/codificacion";


        public const string GET_ALL_REGISTRO_PEDACERIA = URL_BASE + "/envasado/registro-pedaceria";
        public const string POST_REGISTRO_PEDACERIA = URL_BASE + "/envasado/registro-pedaceria";
        
        
        public const string PRINT_ARRANQUE_MAQUINA_ENVASADO = URL_BASE + "/envasado/arranquemaquina/PDF";
        public const string PRINT_ARRANQUE_ENVASADO = URL_BASE + "/envasado/arranque/PDF";
        public const string PRINT_REGISTRO_PEDACERIA = URL_BASE + "/envasado/registro-pedaceria/PDF";
        public const string PRINT_GRANEL_CHECKLIST = URL_BASE + "/envasado-granel/checklist/PDF";
    }
}
