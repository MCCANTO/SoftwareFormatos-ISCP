using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class InsertArranqueMaquinaObservacionCommand : IRequest<StatusResponse<int>>
	{
		public int arranqueMaquinaId { get; set; }
        public string observacion { get; set; }
    }

    public class InsertArranqueMaquinaObservacionCommandHandler : IRequestHandler<InsertArranqueMaquinaObservacionCommand, StatusResponse<int>>
    {

        private readonly IUnitOfWork _uow;

        public InsertArranqueMaquinaObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(InsertArranqueMaquinaObservacionCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {

                    var parametros = new
                    {
                        p_ArranqueMaquinaId = request.arranqueMaquinaId,
                        p_Observacion = request.observacion,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("FR.INSERTAR_ARRANQUE_MAQUINA_OBSERVACION", parametros, commandType: System.Data.CommandType.StoredProcedure);

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

