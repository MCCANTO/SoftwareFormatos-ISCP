using System;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Common.Helpers;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class SaveCondicionesPreviasCommand : SaveCondicionPreviaRequest, IRequest<StatusResponse<int>>
	{
	}

    public class SaveCondicionesPreviasCommandHandler : IRequestHandler<SaveCondicionesPreviasCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public SaveCondicionesPreviasCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(SaveCondicionesPreviasCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var condiciones = DataConvertHelper.ToDataTable<CondicionesRequest>(request.Condiciones);
                    var parametros = new
                    {
                        p_ArranqueMaquinaId = request.ArranqueMaquinaId,
                        p_Condiciones = condiciones,
                        p_Usuario = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("FR.GUARDAR_ARRANQUE_MAQUINA_CONDICIONES_PREVIAS", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    if (id_arranque == 0)
                        return new StatusResponse<int>() { Ok = false, Data = 0, Message = "No se pudo guardar los cambios." };

                    return new StatusResponse<int>() { Ok = true, Data = id_arranque, Message = "Datos guardados correctamente." };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int>() { Ok = false, Data = 0, Message = ex.Message };
                }
            }
        }
    }
}

