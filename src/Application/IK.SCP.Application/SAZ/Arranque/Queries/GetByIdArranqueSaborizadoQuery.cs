using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Queries
{
    public class GetByIdArranqueSaborizadoQuery : IRequest<StatusResponse>
    {
        public int id { get; set; }
    }

    public class GetByIdArranqueSaborizadoQueryHandler : IRequestHandler<GetByIdArranqueSaborizadoQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdArranqueSaborizadoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdArranqueSaborizadoQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.ObtenerArranqueSazonado(request.id);
            return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
        }
    }
}
