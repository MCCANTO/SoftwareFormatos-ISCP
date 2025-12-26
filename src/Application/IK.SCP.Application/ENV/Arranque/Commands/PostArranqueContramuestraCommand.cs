using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueContramuestraCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public int CantidadSobre { get; set; }
        public int CantidadCaja { get; set; }
    }

    public class PostArranqueContramuestraCommandHandler : IRequestHandler<PostArranqueContramuestraCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public PostArranqueContramuestraCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueContramuestraCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_CantidadSobre = request.CantidadSobre,
                        p_CantidadCaja = request.CantidadCaja,
                        p_UsuarioCreacion = _uow.UserName
                    };
            
                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_CONTRAMUESTRA", parametros, commandType: System.Data.CommandType.StoredProcedure);

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
