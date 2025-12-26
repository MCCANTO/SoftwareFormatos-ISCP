using IK.SCP.Domain.Dtos;
using IK.SCP.Domain.Dtos.ENV;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        #region ARRANQUE MAQUINA

        Task<int> GuardarEnvasadoArranqueMaquina(ArranqueMaquinaDto request);
        Task<ArranqueMaquinaCondicionPreviaDto?> ObtenerEnvasadoArranqueMaquinaCondicionPrevia(int id);
        Task<int> GuardarEnvasadoArranqueMaquinaCondicionPrevia(ArranqueMaquinaCondicionPreviaCreateDto request);
        Task<dynamic?> ObtenerEnvasadoArranqueMaquinaVariableBasica(int id);
        Task<int> GuardarEnvasadoArranqueMaquinaVariableBasica(ArranqueMaquinaVariableBasicaCreateDto request);
        Task<int> GuardarEnvasadoArranqueMaquinaObservacion(int arranqueMaquinaId, string observacion);

        #endregion ARRANQUE MAQUINA

        Task<List<dynamic>> ListarEnvasadoRegistroPedaceria(int envasadoraId, string ordenId);
        Task<bool> GuardarEnvasadoRegistroPedaceria(EnvasadoRegistroPedaceriaCreateDto request);
        Task<int> GuardarEnvasadoArranqueEnvasado(ArranqueEnvasadoDto request);
    }
}
