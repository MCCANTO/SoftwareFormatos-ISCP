using IK.SCP.Domain.Dtos;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        Task<IEnumerable<SazonadorDto>> ListarSazonadores();
        Task<IEnumerable<FreidoraDto>> ListarFreidorasXSazonador(int idSazonador);

        Task<bool> GuardarArranqueSazonado(ArranqueSazonadoCreateDto request);
        Task<bool> CerrarArranqueSazonado(int id);
        Task<IEnumerable<dynamic>> ListarArranqueSazonado(int sazonadorId, DateTime? fecha, int? linea, string producto, string sabor);
        Task<dynamic?> ObtenerArranqueSazonado(int arranqueId);
        Task<IEnumerable<dynamic>?> ListarArranqueVerificacionSazonado(int id);
        Task<bool> GuardarArranqueVerificacionSazonado(ArranqueVerificacionSazonadoCreateDto request);
        Task<bool> GuardarArranqueVariableSazonado(ArranqueVariableBasicaSazonadoUpdateDto request);
        Task<bool> GuardarArranqueCondicionSazonado(ArranqueSazonadoCondicionUpdateDto request);
        Task<bool> GuardarArranqueObservacionSazonado(int arranqueId, string observacion);
    }
}
