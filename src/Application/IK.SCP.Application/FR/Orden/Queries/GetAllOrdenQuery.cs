using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllOrdenQuery : IRequest<StatusResponse>
    {
        public int LineaId { get; set; }
        public string? Orden { get; set; }
    }

    public class GetAllOrdenQueryHandler : IRequestHandler<GetAllOrdenQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllOrdenQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<StatusResponse> Handle(GetAllOrdenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarOrdenXFreidora(request.LineaId, request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
