using Dapper;
using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Domain;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class UpdateArranqueMaquinaCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueMaquinaId { get; set; }
    }

    public class UpdateArranqueMaquinaCommandHandler : IRequestHandler<UpdateArranqueMaquinaCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public UpdateArranqueMaquinaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(UpdateArranqueMaquinaCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueMaquinaId = request.ArranqueMaquinaId,
                        p_Usuario = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("FR.CERRAR_ARRANQUE_MAQUINA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    if (id_arranque == 0)
                        return new StatusResponse<int>() { Ok = false, Data = 0, Message = "No se pudo guardar los cambios." };

                    return new StatusResponse<int>() { Ok = true, Data = id_arranque, Message = "Datos guardados correctamente." };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int> { Ok = false, Message = "Error al cerrar arranque de máquina." };
                }
            }
        }
    }
}
