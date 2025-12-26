using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueRevisionCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
    }
    public class PostArranqueRevisionCommandHandler : IRequestHandler<PostArranqueRevisionCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public PostArranqueRevisionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueRevisionCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_REVISION", parametros, commandType: System.Data.CommandType.StoredProcedure);

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
