using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueObservacionCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public string Observacion { get; set; }
    }
    public class PostArranqueObservacionCommandHandler : IRequestHandler<PostArranqueObservacionCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public PostArranqueObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueObservacionCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_Observacion = request.Observacion,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_OBSERVACION", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    return new StatusResponse<int>() { Ok = true, Data = id_arranque };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int>() { Ok = false };
                }

            }
        }
    }
}
