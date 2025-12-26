using IK.SCP.Domain.Dtos;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        Task<IEnumerable<dynamic>> ListarMateriaPrimaAcond();
        Task<IEnumerable<dynamic>> ListarProcesoXMateriaPrimaAcond(int materiaPrimaId);
        Task<IEnumerable<dynamic>> ListarOrdenAcond(string orden, DateTime? fechaInicio, DateTime? fechaFin, int materiaPrimaId);
        Task<dynamic> ObtenerOrdenPorIdAcond(string orden);
        Task<Tuple<bool, string>> InsertarOrdenAcond(OrdenAcondCreateDto request);


        Task<IEnumerable<dynamic>> ListarArranqueMaizAcond(string ordenId);
        Task<dynamic> ObtenerArranqueMaizPorIdAcond(int arranqueMaizId);
        Task<dynamic> ObtenerArranqueMaizAbiertoAcond(string ordenId);
        Task<bool> InsertarArranqueMaizAcond(string ordenId);
        Task<bool> CerrarArranqueMaizAcond(int arranqueMaizId);
        Task<IEnumerable<dynamic>?> ListarArranqueMaizVerificacionAcond(int id);
        Task<bool> GuardarArranqueMaizVerificacionAcond(ArranqueMaizVerificacionAcondCreateDto request);
        Task<bool> GuardarArranqueMaizVariableAcond(ArranqueMaizVariableBasicaAcondUpdateDto request);
        Task<bool> GuardarArranqueMaizCondicionAcond(ArranqueMaizCondicionAcondUpdateDto request);
        Task<bool> GuardarArranqueMaizObservacionAcond(int arranqueMaizId, string observacion);



        Task<IEnumerable<dynamic>> ListarControlMaizMateriaPrimaAcond(string ordenId);
        Task<bool> GuardarControlMaizMateriaPrimaAcond(PeladoMaizMateriaPrimaAcondCreateDto request);
        Task<IEnumerable<dynamic>> ListarControlMaizInsumoAcond(string ordenId);
        Task<bool> GuardarControlMaizInsumoAcond(PeladoMaizInsumoAcondUpdateDto request);
        Task<dynamic> ListarControlMaizPeladoAcond(string ordenId);
        Task<bool> GuardarControlMaizPeladoAcond(PeladoMaizControlAcondUpdateDto request);
        Task<dynamic> ListarControlMaizRemojoAcond(string ordenId);
        Task<bool> GuardarControlMaizRemojoAcond(RemojoMaizControlAcondUpdateDto request);
        Task<dynamic> ListarControlMaizSancochadoAcond(string ordenId);
        Task<bool> GuardarControlMaizSancochadoAcond(SancochadoMaizControlAcondUpdateDto request);
        Task<dynamic> ListarObservacionMaizPeladoAcond(string ordenId);
        Task<bool> GuardarObservacionMaizPeladoAcond(string ordenId, string observacion);


        Task<dynamic> ListarControlReposoMaizAcond(string ordenId);
        Task<dynamic> ObtenerDataSancochadoControlReposoMaizAcond(string ordenId, int numeroBatch);
        Task<bool> GuardarControlReposoMaizAcond(ReposoMaizControlAcondCreateDto request);

        Task<dynamic> ListarControlRemojoHabaAcond(string ordenId);
        Task<bool> GuardarControlRemojoHabaAcond(RemojoHabaControlAcondCreateDto request);

        Task<IEnumerable<dynamic>> ListarArranqueLavadoTuberculoAcond(string ordenId);
        Task<dynamic?> ObtenerArranqueLavadoTuberculoPorIdAcond(int arranqueLavadoTuberculoId);
        Task<dynamic?> ObtenerArranqueLavadoTuberculoAbiertoAcond(string ordenId);
        Task<bool> InsertarArranqueLavadoTuberculoAcond(string ordenId);
        Task<bool> CerrarArranqueLavadoTuberculoAcond(int arranqueLavadoTuberculoId);
        Task<IEnumerable<dynamic>?> ListarArranqueLavadoTuberculoVerificacionAcond(int id);
        Task<bool> GuardarArranqueLavadoTuberculoVerificacionAcond(ArranqueLavadoTuberculoVerificacionAcondCreateDto request);
        Task<bool> GuardarArranqueLavadoTuberculoCondicionAcond(ArranqueLavadoTuberculoCondicionAcondUpdateDto request);


        Task<IEnumerable<dynamic>> ListarControlRayosXAcond(string periodo);
        Task<bool> GuardarControlRayosXAcond(ControlRayosXAcondCreateDto request);
        Task<bool> RevisarControlRayosXAcond(List<int> ids);


        Task<IEnumerable<dynamic>> ListarArranqueElectroporadorAcond(string ordenId);
        Task<dynamic?> ObtenerArranqueElectroporadorPorIdAcond(int arranqueElectroporadorId);
        Task<dynamic?> ObtenerArranqueElectroporadorAbiertoAcond(string ordenId);
        Task<bool> InsertarArranqueElectroporadorAcond(string ordenId);
        Task<bool> CerrarArranqueElectroporadorAcond(int arranqueElectroporadorId);
        Task<IEnumerable<dynamic>?> ListarArranqueElectroporadorVerificacionAcond(int id);
        Task<bool> GuardarArranqueElectroporadorVerificacionAcond(ArranqueElectroporadorVerificacionAcondCreateDto request);
        Task<bool> GuardarArranqueElectroporadorCondicionAcond(ArranqueElectroporadorCondicionBasicaAcondCreateDto request);
        Task<bool> GuardarArranqueElectroporadorVariableBasicaAcond(ArranqueElectroporadorVariableBasicaAcondUpdateDto request);

        Task<dynamic?> ObtenerControlPefAcond(string ordenId);
        Task<dynamic?> ObtenerControlPefPorIdAcond(int id);
        Task<bool> InsertarControlPefAcond(string ordenId);
        Task<bool> ActualizarControlPefAcond(ControlPefUpdateAcondDto request);
        Task<dynamic?> ListarControlPefCondicionDetalleAcond(int id);
        Task<bool> GuardarControlPefCondicionAcond(ControlPefCondicionBasicaAcondCreateDto request);
        Task<bool> GuardarControlPefFuerzaCorteAcond(ControlPefFuerzaCorteAcondCreateDto request);
        Task<dynamic?> ListarControlPefFuerzaCorteDetalleAcond(int id);
        Task<bool> GuardarControlPefTiempoAcond(ControlPefTiempoAcondCreateDto request);
    }
}
