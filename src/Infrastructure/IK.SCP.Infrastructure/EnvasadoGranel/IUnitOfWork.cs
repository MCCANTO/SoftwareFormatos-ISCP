using IK.SCP.Domain.Dtos;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        #region CHECKLIST GRANEL

        Task<IEnumerable<GetAllChecklistGranelDto>> GetAllChecklistGranel(int envasadoraId, string orden);
        Task<int> CreateChecklistGranel(int envasadoraId, string orden);
        Task<bool> UpdateChecklistGranel(ChecklistGranelUpdateDto request);
        Task<bool> CloseChecklistGranel(int id);
        Task<ChecklistGranelDto?> ObtenerChecklistGranel(int envasadoraId, string orden);
        Task<ChecklistGranelDto?> ObtenerChecklistGranelPorId(int id);

        Task<IEnumerable<EspecificacionGranelDto>> ListarChecklistGranelEspecificaciones(int id);
        Task<bool> GuardarEspecificacionesGranel(List<EspecificacionGranelUpdateDto> especificaciones);
        
        Task<CondicionOperativaGranelDto?> ListarCondicionOperativaGranelDetalle(int arranqueGranelCondicionOperativaId);
        Task<bool> GuardarCondicionOperativaGranel(CondicionOperativaGranelCreateDto request);

        Task<IEnumerable<CondicionProcesoGranelDto>> ListarCondicionProcesoGranelDetalle(int arranqueGranelCondicionProcesoId);
        Task<bool> GuardarCondicionProcesoGranel(CondicionProcesoGranelCreateDto request);

        Task<bool> GuardarChecklistObservacionGranel(int arranqueGranelId, string observacion);

        Task<bool> GuardarChecklistRevisionGranel(int arranqueGranelId);

        #endregion CHECKLIST GRANEL

        #region CONTROL GRANEL

        Task<ControlParametroGranelDto?> ListarControlGranel(int envasadoraId, string orden);
        Task<dynamic> ListarParametroControlGranel();
        Task<bool> GuardarControlGranel(int envasadoraId, string orden, object parametros);
        Task<IEnumerable<dynamic>> ListarObservacionControlGranel(int envasadoraId, string orden);
        Task<bool> GuardarObservacionControlGranel(int envasadoraId, string orden, string observacion);

        #endregion CONTROL GRANEL

        #region EVALUACION PT GRANEL

        Task<IEnumerable<dynamic>> ListarEvaluacionPTGranel(int envasadoraId, string orden);
        Task<dynamic> ObtenerEvaluacionPTGranel(int id);
        Task<bool> GuardarEvaluacionPTGranel(EvaluactionPTCreateDto request);

        #endregion EVALUACION PT GRANEL

        #region CODIFICACION GRANEL

        Task<IEnumerable<dynamic>> ListarCodificacionGranel(int envasadoraId, string orden);
        Task<bool> GuardarCodificacionGranel(CodificacionCajaGranelCreateDto request);

        #endregion CODIFICACION GRANEL
    }
}
