using IK.SCP.Domain.Dtos;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        Task<IEnumerable<OrdenFrituraDto>> ListarOrdenXFreidora(int idLinea, string orden = "");
        Task<IEnumerable<dynamic>> ListarControlAceite(DateTime desde, DateTime hasta, int lineaId, string ordenId);
        Task<bool> GuardarControlAceite(ControlAceiteCreateDto request);
        Task<IEnumerable<dynamic>> ListarDefectoCaracterizacion(string articulo);
        Task<IEnumerable<dynamic>> ListarRegistroCaracterizacion(string ordenId);
        Task<bool> GuardarRegistroCaracterizacion(RegistroCaracterizacionCreateDto request);
    }
}
