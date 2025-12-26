using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Text.Json;

namespace IK.SCP.Application.FR.Commands.Insert
{
    public class InsertArranqueMaquinaVerificacionEquipoCommand : InsertArranqueMaquinaVerificacionEquipoRequest, IRequest<StatusResponse<int>>
    {
    }

    public class InsertArranqueMaquinaVerificacionEquipoCommandHandler : IRequestHandler<InsertArranqueMaquinaVerificacionEquipoCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public InsertArranqueMaquinaVerificacionEquipoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(InsertArranqueMaquinaVerificacionEquipoCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueMaquinaVerificacionEquipoCabId = request.ArranqueMaquinaVerificacionEquipoCabId,
                        p_ArranqueMaquinaId = request.ArranqueMaquinaId,
                        p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                        p_Usuario = _uow.UserName
                    };

                    var id = await cnn.ExecuteScalarAsync<int>("FR.GUARDAR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    if (id == 0)
                        return new StatusResponse<int>() { Ok = false, Data = 0, Message = "No se pudo guardar los cambios." };

                    return new StatusResponse<int>() { Ok = true, Data = id, Message = "Datos guardados correctamente." };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int>() { Ok = false, Data = 0, Message = ex.Message };
                }
            }
        }
    }
}
