using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetByIdOrdenQuery : IRequest<StatusResponse<object>>
    {
        public int LineaId { get; set; }
        public string Orden { get; set; }
    }

    public class GetByIdOrdenQueryHandlr : IRequestHandler<GetByIdOrdenQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdOrdenQueryHandlr(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetByIdOrdenQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var orden = await cnn.QueryFirstOrDefaultAsync<dynamic>("FR.OBTENER_INFO_ORDEN", new { p_OrdenId  = request.Orden, p_LineaId = request.LineaId } , commandType: CommandType.StoredProcedure);

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = orden
                };
            }
        }
    }
}
