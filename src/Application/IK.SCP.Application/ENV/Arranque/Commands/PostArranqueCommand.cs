using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueCommand : IRequest<StatusResponse<int>>
    {
        public int envasadoraId { get; set; }
        public string ordenId { get; set; }
    }

    public class PostArranqueCommandHandler : IRequestHandler<PostArranqueCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public PostArranqueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {

                    var parametros = new
                    {
                        p_EnvasadoraId = request.envasadoraId,
                        p_OrdenId = request.ordenId,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.OBTENER_ARRANQUE_ACTIVO", parametros, commandType: CommandType.StoredProcedure);

                    return new StatusResponse<int> { Ok = true, Data = id_arranque };
                }
                catch
                {
                    return new StatusResponse<int> { Ok = false, Message = "Error al registrar arranque de máquina." };
                }
            }
        }
    }
}
