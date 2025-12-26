using IK.SCP.Domain.Dtos;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        #region CHECKLIST

        Task<bool> ValidarArticuloMezclaBlending(string articulo);
        Task<IEnumerable<dynamic>> ListarComponentesBlending(string orden);
        Task<IEnumerable<dynamic>> ListarArranquesBlending(string orden);
        Task<dynamic> ObtenerArranqueActivoBlending(string orden);
        Task<dynamic> ObtenerArranquePorIdBlending(int id);
        Task<bool> GuardarArranqueBlending(string orden);
        Task<bool> CerrarArranqueBlending(int id);

        Task<IEnumerable<dynamic>?> ListarArranqueVerificacionBlending(int id);
        Task<bool> GuardarArranqueVerificacionBlending(ArranqueVerificacionBlendingCreateDto request);
        Task<bool> GuardarArranqueCondicionBlending(ArranqueCondicionBlendingUpdateDto request);
        Task<bool> GuardarArranqueObservacionBlending(int blendingArranqueId, string observacion);

        #endregion CHECKLIST

        #region CONTROL

        Task<IEnumerable<dynamic>> ListarControlComponentesBlending(string orden);
        Task<bool> InsertarControlComponentesBlending(string orden);
        Task<bool> ActualizarControlComponentesBlending(string orden, List<ControlComponenteBlendingUpdateDto> componentes);
        Task<dynamic> ListarControlBlending(string orden);
        Task<bool> InsertarControlBlending(string orden, string observacion, List<ControlComponenteBlendingDto> componentes);
        Task<IEnumerable<dynamic>> ListarControlMermaBlending(string orden);
        Task<bool> ActualizarControlMermaBlending(string orden, List<ControlMermaBlendingUpdateDto> componentes);

        #endregion CONTROL
    }
}
