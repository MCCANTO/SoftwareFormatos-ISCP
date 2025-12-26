using IK.SCP.App.Routes;
using IK.SCP.Application.ACO.Commands;
using IK.SCP.Application.ACO.Queries;
using IK.SCP.Application.PDF.Fritura.Dao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    public class AcondicionamientoController : BaseApiController
    {
        #region GENERALES

        [HttpGet(ApiRoutes.GET_ALL_ACOND_MATERIA_PRIMA)]
        public async Task<IActionResult> GetAllMateriaPrimaAcond()
        {
            var result = await Mediator.Send(new GetAllMateriaPrimaAcondQuery());
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ALL_ACOND_PROCESO_MATERIA_PRIMA)]
        public async Task<IActionResult> GetAllProcesoMateriaPrimaAcond([FromQuery] GetAllProcesoMateriaPrimaAcondQuery getAllProcesoMateriaPrimaAcondQuery)
        {
            var result = await Mediator.Send(getAllProcesoMateriaPrimaAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ALL_ACOND_ORDEN)]
        public async Task<IActionResult> GetAllOrdenAcond([FromQuery] GetAllOrdenAcondQuery getAllOrdenAcondQuery)
        {
            var result = await Mediator.Send(getAllOrdenAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ACOND_ORDEN)]
        public async Task<IActionResult> GetByIdOrdenAcond([FromRoute] GetByIdOrdenAcondQuery getByIdOrdenAcondQuery)
        {
            var result = await Mediator.Send(getByIdOrdenAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_ORDEN)]
        public async Task<IActionResult> InsertOrdenAcond([FromBody] InsertOrdenAcondCommand insertOrdenAcondCommand)
        {
            var result = await Mediator.Send(insertOrdenAcondCommand);
            return Ok(result);
        }

        #endregion GENERALES

        #region CHECKLIST DE ARRANQUE DE MAIZ

        [HttpGet(ApiRoutes.GET_ALL_ACOND_ARRANQUE_MAIZ)]
        public async Task<IActionResult> GetAllArranqueMaizAcond([FromQuery] GetAllArranqueMaizAcondQuery getAllArranqueMaizAcondQuery)
        {
            var result = await Mediator.Send(getAllArranqueMaizAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ACOND_ARRANQUE_MAIZ)]
        public async Task<IActionResult> GetByIdArranqueMaizAcond([FromRoute] GetByIdArranqueMaizAcondQuery getByIdArranqueMaizAcondQuery)
        {
            var result = await Mediator.Send(getByIdArranqueMaizAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_ARRANQUE_MAIZ_ACTIVO)]
        public async Task<IActionResult> GetArranqueMaizActivoAcond([FromRoute] GetArranqueMaizActivoAcondQuery getArranqueMaizActivoAcondQuery)
        {
            var result = await Mediator.Send(getArranqueMaizActivoAcondQuery);
            return Ok(result);
        }


        [HttpPost(ApiRoutes.POST_ACOND_ARRANQUE_MAIZ)]
        public async Task<IActionResult> InsertArranqueMaizAcond([FromBody] InsertArranqueMaizAcondCommand insertArranqueMaizAcondCommand)
        {
            var result = await Mediator.Send(insertArranqueMaizAcondCommand);
            return Ok(result);
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_MAIZ_CIERRE)]
        public async Task<IActionResult> UpdateArranqueMaizCierreAcond([FromRoute] UpdateArranqueMaizCierreAcondCommand updateArranqueMaizCierreAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueMaizCierreAcondCommand));
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_MAIZ_CONDICION)]
        public async Task<IActionResult> UpdateArranqueMaizCondicionAcond([FromBody] UpdateArranqueMaizCondicionAcondCommand updateArranqueMaizCondicionAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueMaizCondicionAcondCommand));
        }


        [HttpGet(ApiRoutes.GET_ACOND_ARRANQUE_MAIZ_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> GetAllArranqueVerificacionDetalleAcond([FromRoute] GetAllArranqueVerificacionDetalleAcondQuery getAllArranqueVerificacionDetalleAcondQuery)
        {
            return Ok(await Mediator.Send(getAllArranqueVerificacionDetalleAcondQuery));
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_MAIZ_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> UpdateArranqueMaizVerificacionDetalleAcond([FromBody] UpdateArranqueMaizVerificacionDetalleAcondCommand updateArranqueMaizVerificacionDetalleAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueMaizVerificacionDetalleAcondCommand));
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_MAIZ_VARIABLE_BASICA)]
        public async Task<IActionResult> UpdateArranqueMaizVariableAcond([FromBody] UpdateArranqueMaizVariableAcondCommand updateArranqueMaizVariableAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueMaizVariableAcondCommand));
        }


        [HttpPost(ApiRoutes.POST_ACOND_ARRANQUE_MAIZ_OBSERVACION)]
        public async Task<IActionResult> InsertArranqueMaizObservacionAcond([FromBody] InsertArranqueMaizObservacionAcondCommand insertArranqueMaizObservacionAcondCommand)
        {
            return Ok(await Mediator.Send(insertArranqueMaizObservacionAcondCommand));
        }

        #endregion CHECKLIST DE ARRANQUE DE MAIZ

        #region CONTROL MAIZ

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_MATERIA_PRIMA)]
        public async Task<IActionResult> GetControlMaizMateriaPrimaAcond([FromQuery] GetControlMaizMateriaPrimaAcondQuery getControlMaizMateriaPrimaAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizMateriaPrimaAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_MATERIA_PRIMA)]
        public async Task<IActionResult> InsertControlMaizMateriaPrimaAcond([FromBody] InsertControlMaizMateriaPrimaAcondCommand insertControlMaizMateriaPrimaAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizMateriaPrimaAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_INSUMO)]
        public async Task<IActionResult> GetControlMaizInsumoAcond([FromQuery] GetControlMaizInsumoAcondQuery getControlMaizInsumoAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizInsumoAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_INSUMO)]
        public async Task<IActionResult> InsertControlMaizInsumoAcond([FromBody] InsertControlMaizInsumoAcondCommand insertControlMaizInsumoAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizInsumoAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_OBSERVACION)]
        public async Task<IActionResult> GetControlMaizObservacionAcond([FromQuery] GetControlMaizObservacionAcondQuery getControlMaizObservacionAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizObservacionAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_OBSERVACION)]
        public async Task<IActionResult> InsertControlMaizObservacionAcond([FromBody] InsertControlMaizObservacionAcondCommand insertControlMaizObservacionAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizObservacionAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_PELADO)]
        public async Task<IActionResult> GetControlMaizPeladoAcond([FromQuery] GetControlMaizPeladoAcondQuery getControlMaizPeladoAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizPeladoAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_PELADO)]
        public async Task<IActionResult> InsertControlMaizPeladoAcond([FromBody] InsertControlMaizPeladoAcondCommand insertControlMaizPeladoAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizPeladoAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_REMOJO)]
        public async Task<IActionResult> GetControlMaizRemojoAcond([FromQuery] GetControlMaizRemojoAcondQuery getControlMaizRemojoAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizRemojoAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_REMOJO)]
        public async Task<IActionResult> InsertControlMaizRemojoAcond([FromBody] InsertControlMaizRemojoAcondCommand insertControlMaizRemojoAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizRemojoAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_SANCOCHADO)]
        public async Task<IActionResult> GetControlMaizSancochadoAcond([FromQuery] GetControlMaizSancochadoAcondQuery getControlMaizSancochadoAcondQuery)
        {
            var result = await Mediator.Send(getControlMaizSancochadoAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_SANCOCHADO)]
        public async Task<IActionResult> InsertControlMaizSancochadoAcond([FromBody] InsertControlMaizSancochadoAcondCommand insertControlMaizSancochadoAcondCommand)
        {
            var result = await Mediator.Send(insertControlMaizSancochadoAcondCommand);
            return Ok(result);
        }

        #endregion CONTROL MAIZ

        #region CONTROL REPOSO MAIZ

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_REPOSO)]
        public async Task<IActionResult> GetAllControlReposoMaizAcond([FromQuery] GetAllControlReposoMaizAcondQuery getAllControlReposoMaizAcondQuery)
        {
            var result = await Mediator.Send(getAllControlReposoMaizAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_MAIZ_SANCOCHADO_BATCH)]
        public async Task<IActionResult> GetSancochadoByBatchControlReposoMaizAcond([FromQuery] GetSancochadoByBatchControlReposoMaizAcondQuery getSancochadoByBatchControlReposoMaizAcondQuery)
        {
            var result = await Mediator.Send(getSancochadoByBatchControlReposoMaizAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_MAIZ_REPOSO)]
        public async Task<IActionResult> InsertControlReposoMaizAcond([FromBody] InsertControlReposoMaizAcondCommand insertControlReposoMaizAcondCommand)
        {
            var result = await Mediator.Send(insertControlReposoMaizAcondCommand);
            return Ok(result);
        }

        #endregion CONTROL REPOSO MAIZ
        
        #region CONTROL REMOJO HABA

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_HABA_REMOJO)]
        public async Task<IActionResult> GetAllControlRemojoHabaAcond([FromQuery] GetAllControlRemojoHabaAcondQuery getAllControlRemojoHabaAcondQuery)
        {
            var result = await Mediator.Send(getAllControlRemojoHabaAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_HABA_REMOJO)]
        public async Task<IActionResult> InsertControlRemojoHabaAcond([FromBody] InsertControlRemojoHabaAcondCommand insertControlRemojoHabaAcondCommand)
        {
            var result = await Mediator.Send(insertControlRemojoHabaAcondCommand);
            return Ok(result);
        }

        #endregion CONTROL REMOJO HABA

        #region CHECKLIST ARRANQUE LAVADO TUBERCULO

        [HttpGet(ApiRoutes.GET_ALL_ACOND_ARRANQUE_LAVADO_TUBERCULO)]
        public async Task<IActionResult> GetAllArranqueLavadoTuberculoAcond([FromQuery] GetAllArranqueLavadoTuberculoAcondQuery getAllArranqueLavadoTuberculoAcondQuery)
        {
            var result = await Mediator.Send(getAllArranqueLavadoTuberculoAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ACOND_ARRANQUE_LAVADO_TUBERCULO)]
        public async Task<IActionResult> GetByIdArranqueLavadoTuberculoAcond([FromRoute] GetByIdArranqueLavadoTuberculoAcondQuery getByIdArranqueLavadoTuberculoAcondQuery)
        {
            var result = await Mediator.Send(getByIdArranqueLavadoTuberculoAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_ARRANQUE_LAVADO_TUBERCULO_ACTIVO)]
        public async Task<IActionResult> GetArranqueLavadoTuberculoActivoAcond([FromRoute] GetArranqueLavadoTuberculoActivoAcondQuery getArranqueLavadoTuberculoActivoAcondQuery)
        {
            var result = await Mediator.Send(getArranqueLavadoTuberculoActivoAcondQuery);
            return Ok(result);
        }


        [HttpPost(ApiRoutes.POST_ACOND_ARRANQUE_LAVADO_TUBERCULO)]
        public async Task<IActionResult> InsertArranqueLavadoTuberculoAcond([FromBody] InsertArranqueLavadoTuberculoAcondCommand insertArranqueLavadoTuberculoAcondCommand)
        {
            var result = await Mediator.Send(insertArranqueLavadoTuberculoAcondCommand);
            return Ok(result);
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_CIERRE)]
        public async Task<IActionResult> UpdateArranqueLavadoTuberculoCierreAcond([FromBody] UpdateArranqueLavadoTuberculoCierreAcondCommand updateArranqueLavadoTuberculoCierreAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueLavadoTuberculoCierreAcondCommand));
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_CONDICION)]
        public async Task<IActionResult> UpdateArranqueLavadoTuberculoCondicionAcond([FromBody] UpdateArranqueLavadoTuberculoCondicionAcondCommand updateArranqueLavadoTuberculoCondicionAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueLavadoTuberculoCondicionAcondCommand));
        }


        [HttpGet(ApiRoutes.GET_ACOND_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> GetAllArranqueLavadoTuberculoVerificacionDetalleAcond([FromRoute] GetAllArranqueLavadoTuberculoVerificacionDetalleAcondQuery getAllArranqueLavadoTuberculoVerificacionDetalleAcondQuery)
        {
            return Ok(await Mediator.Send(getAllArranqueLavadoTuberculoVerificacionDetalleAcondQuery));
        }


        [HttpPut(ApiRoutes.PUT_ACOND_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> UpdateArranqueLavadoTuberculoVerificacionDetalleAcond([FromBody] UpdateArranqueLavadoTuberculoVerificacionDetalleAcondCommand updateArranqueLavadoTuberculoVerificacionDetalleAcondCommand)
        {
            return Ok(await Mediator.Send(updateArranqueLavadoTuberculoVerificacionDetalleAcondCommand));
        }

        #endregion CHECKLIST ARRANQUE LAVADO TUBERCULO

        #region CONTROL RAYOS X

        [HttpGet(ApiRoutes.GETALL_ACOND_CONTROL_RAYOS_X)]
        public async Task<IActionResult> GetAllControlRayosXAcond([FromQuery] GetAllControlRayosXAcondQuery getAllControlRayosXAcondQuery)
        {
            var result = await Mediator.Send(getAllControlRayosXAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_RAYOS_X)]
        public async Task<IActionResult> InsertControlRayosXAcond([FromBody] InsertControlRayosXAcondCommand insertControlRayosXAcondCommand)
        {
            var result = await Mediator.Send(insertControlRayosXAcondCommand);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CONTROL_RAYOS_X_REVISION)]
        public async Task<IActionResult> UpdateControlRayosRevisionXAcond([FromBody] UpdateControlRayosRevisionXAcondQuery updateControlRayosRevisionXAcondQuery)
        {
            var result = await Mediator.Send(updateControlRayosRevisionXAcondQuery);
            return Ok(result);
        }

        #endregion CONTROL RAYOS X

        #region CHECKLIST ELECTROPORADOR

        [HttpGet(ApiRoutes.GETALL_ACOND_CHECKLIST_ELECTROPORADOR)]
        public async Task<IActionResult> GetAllChecklistElectroporadorAcond([FromQuery] GetAllChecklistElectroporadorAcondQuery getAllChecklistElectroporadorAcondQuery)
        {
            var result = await Mediator.Send(getAllChecklistElectroporadorAcondQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ACOND_CHECKLIST_ELECTROPORADOR)]
        public async Task<IActionResult> GetByIdChecklistElectroporadorAcond([FromRoute] GetByIdChecklistElectroporadorAcondQuery getByIdChecklistElectroporadorAcondQuery)
        {
            var result = await Mediator.Send(getByIdChecklistElectroporadorAcondQuery);
            System.Diagnostics.Debug.WriteLine("Ingreso al apartado de electroporador");
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CHECKLIST_ELECTROPORADOR_ACTIVO)]
        public async Task<IActionResult> GetChecklistElectroporadorActivoAcond([FromRoute] GetChecklistElectroporadorActivoAcondQuery getChecklistElectroporadorActivoAcondQuery)
        {
            var result = await Mediator.Send(getChecklistElectroporadorActivoAcondQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ACOND_CHECKLIST_ELECTROPORADOR)]
        public async Task<IActionResult> InsertChecklistElectroporadorAcond([FromBody] InsertChecklistElectroporadorAcondCommand insertChecklistElectroporadorAcondCommand)
        {
            var result = await Mediator.Send(insertChecklistElectroporadorAcondCommand);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CHECKLIST_ELECTROPORADOR)]
        public async Task<IActionResult> UpdateChecklistElectroporadorAcond([FromBody] UpdateChecklistElectroporadorAcondCommand updateChecklistElectroporadorAcondCommand)
        {
            var result = await Mediator.Send(updateChecklistElectroporadorAcondCommand);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CHECKLIST_ELECTROPORADOR_CONDICION)]
        public async Task<IActionResult> UpdateChecklistElectroporadorCondicionAcond([FromBody] UpdateChecklistElectroporadorCondicionAcondCommand updateChecklistElectroporadorCondicionAcondCommand)
        {
            var result = await Mediator.Send(updateChecklistElectroporadorCondicionAcondCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ACOND_CHECKLIST_ELECTROPORADOR_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> GetAllChecklistElectroporadorVerificacionDetalleAcond([FromRoute] GetAllChecklistElectroporadorVerificacionDetalleAcondQuery getAllChecklistElectroporadorVerificacionDetalleAcondQuery)
        {
            var result = await Mediator.Send(getAllChecklistElectroporadorVerificacionDetalleAcondQuery);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CHECKLIST_ELECTROPORADOR_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> UpdateChecklistElectroporadorVerificacionDetalleAcond([FromBody] UpdateChecklistElectroporadorVerificacionDetalleAcondCommand updateChecklistElectroporadorVerificacionDetalleAcondCommand)
        {
            var result = await Mediator.Send(updateChecklistElectroporadorVerificacionDetalleAcondCommand);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CHECKLIST_ELECTROPORADOR_VARIABLE_BASICA)]
        public async Task<IActionResult> UpdateChecklistElectroporadorVariableBasicaAcond([FromBody] UpdateChecklistElectroporadorVariableBasicaAcondCommand updateChecklistElectroporadorVariableBasicaAcondCommand)
        {
            var result = await Mediator.Send(updateChecklistElectroporadorVariableBasicaAcondCommand);
            return Ok(result);
        }

        #endregion CHECKLIST ELECTROPORADOR

        #region CONTROL TRATAMIENTO PEF

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_TRATAMIENTO_PEF)]
        public async Task<IActionResult> GetControlTratamientoPefAcond([FromQuery] GetControlTratamientoPefAcondQuery getControlTratamientoPefAcondQuery)
        {
            return Ok(await Mediator.Send(getControlTratamientoPefAcondQuery));
        }

        //[HttpGet(ApiRoutes.GET_BY_ID_ACOND_CONTROL_TRATAMIENTO_PEF)]
        //public async Task<IActionResult> GetByIdControlTratamientoPefAcond([FromRoute] GetByIdControlTratamientoPefAcondQuery getByIdControlTratamientoPefAcondQuery)
        //{
        //    return Ok(await Mediator.Send(getByIdControlTratamientoPefAcondQuery));
        //}

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_TRATAMIENTO_PEF)]
        public async Task<IActionResult> InsertControlTratamientoPefAcond([FromBody] InsertControlTratamientoPefAcondCommand insertControlTratamientoPefAcondCommand)
        {
            return Ok(await Mediator.Send(insertControlTratamientoPefAcondCommand));
        }

        [HttpPut(ApiRoutes.PUT_ACOND_CONTROL_TRATAMIENTO_PEF)]
        public async Task<IActionResult> UpdateControlTratamientoPefAcond([FromBody] UpdateControlTratamientoPefAcondCommand updateControlTratamientoPefAcondCommand)
        {
            return Ok(await Mediator.Send(updateControlTratamientoPefAcondCommand));
        }

        //[HttpPut(ApiRoutes.PUT_ACOND_CONTROL_TRATAMIENTO_PEF_CIERRE)]
        //public async Task<IActionResult> UpdateControlTratamientoPefCierrAcond([FromBody] UpdateControlTratamientoPefCierreAcondCommand updateControlTratamientoPefCierreAcondCommand)
        //{
        //    return Ok(await Mediator.Send(updateControlTratamientoPefCierreAcondCommand));
        //}

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_TRATAMIENTO_PEF_CONDICION_PREVIA_DETALLE)]
        public async Task<IActionResult> GetControlTratamientoPefCondicionPreviaDetalleAcond([FromRoute] GetControlTratamientoPefCondicionPreviaDetalleAcondQuery getControlTratamientoPefCondicionPreviaDetalleAcondQuery)
        {
            return Ok(await Mediator.Send(getControlTratamientoPefCondicionPreviaDetalleAcondQuery));
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_TRATAMIENTO_PEF_CONDICION_PREVIA)]
        public async Task<IActionResult> InsertControlTratamientoPefCondicionPreviaAcond([FromBody] InsertControlTratamientoPefCondicionPreviaAcondCommand insertControlTratamientoPefCondicionPreviaAcondCommand)
        {
            return Ok(await Mediator.Send(insertControlTratamientoPefCondicionPreviaAcondCommand));
        }

        [HttpGet(ApiRoutes.GET_ACOND_CONTROL_TRATAMIENTO_PEF_FUERZA_CORTE_DETALLE)]
        public async Task<IActionResult> GetControlTratamientoPefVerificacionDetalleAcond([FromRoute] GetControlTratamientoPefVerificacionDetalleAcondQuery getControlTratamientoPefVerificacionDetalleAcondQuery)
        {
            return Ok(await Mediator.Send(getControlTratamientoPefVerificacionDetalleAcondQuery));
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_TRATAMIENTO_PEF_FUERZA_CORTE)]
        public async Task<IActionResult> InsertControlTratamientoPefFuerzaCorteAcond([FromBody] InsertControlTratamientoPefFuerzaCorteAcondCommand insertControlTratamientoPefFuerzaCorteAcondCommand)
        {
            return Ok(await Mediator.Send(insertControlTratamientoPefFuerzaCorteAcondCommand));
        }

        [HttpPost(ApiRoutes.POST_ACOND_CONTROL_TRATAMIENTO_PEF_TIEMPO)]
        public async Task<IActionResult> InsertControlTratamientoPefTiempoAcond([FromBody] InsertControlTratamientoPefTiempoAcondCommand insertControlTratamientoPefTiempoAcondCommand)
        {
            return Ok(await Mediator.Send(insertControlTratamientoPefTiempoAcondCommand));
        }

        #endregion CONTROL TRATAMIENTO PEF

        #region GENERATE PDF
        [HttpGet(ApiRoutes.PRINT_ACOND_CONTROL_MAIZ_REPOSO)]
        public async Task<IActionResult> PrintDocumentAcondControlMaizReposo([FromQuery] ControlReposoMaiz controlReposoMaiz)
        {
            var response = Mediator.Send(controlReposoMaiz);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CONTROL_HABA_REMOJO)]
        public async Task<IActionResult> PrintDocumentAcondControlHabasRemojo([FromQuery] ControlRemojoHabas controlRemojoHabas)
        {
            var response = Mediator.Send(controlRemojoHabas);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CONTROL_MAIZ_PELADO_REMOJO_SANCOCHADO)]
        public async Task<IActionResult> PrintDocumentAcondControlMaiz([FromQuery] ControlPeladoRemojoSancochadoMaiz controlPeladoRemojoSancochadoMaiz)
        {
            var response = Mediator.Send(controlPeladoRemojoSancochadoMaiz);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CONTROL_TRATAMIENTO_PEF)]
        public async Task<IActionResult> PrintDocumentAcondControlTratamientoPEF([FromQuery] ControlTratamientoPEF controlTratamientoPef)
        {
            var response = Mediator.Send(controlTratamientoPef);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CHECKLIST_ELECTROPORADOR)]
        public async Task<IActionResult> PrintDocumentAcondChecklistElectroporador([FromQuery] ChecklistArranqueElectroporador checklistArranqueElectroporador)
        {
            var response = Mediator.Send(checklistArranqueElectroporador);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CHECKLIST_LAVADO_TUBERCULO)]
        public async Task<IActionResult> PrintDocumentAcondChecklistLavadoTuberculos([FromQuery] ChecklistArranqueLavadoTuberculos checklistArranqueLavadoTuberculos)
        {
            var response = Mediator.Send(checklistArranqueLavadoTuberculos);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_CONTROL_RAYOS_X)]
        public async Task<IActionResult> PrintDocumentControlRayosXAcond([FromQuery] ControlRayosXAcond controlRayosXAcond)
        {
            var response = Mediator.Send(controlRayosXAcond);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_ACOND_ARRANQUE_MAIZ)]
        public async Task<IActionResult> PrintDocumentChecklistArranqueMaiz([FromQuery] ChecklistArranqueMaiz checklistArranqueMaiz)
        {
            var response = Mediator.Send(checklistArranqueMaiz);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        #endregion
    }
}
