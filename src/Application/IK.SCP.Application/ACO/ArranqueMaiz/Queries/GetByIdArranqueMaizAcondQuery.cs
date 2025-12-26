using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetByIdArranqueMaizAcondQuery : IRequest<StatusResponse>
    {
        public int ArranqueMaizId { get; set; }
    }

    public class GetByIdArranqueMaizAcondQueryHandler : IRequestHandler<GetByIdArranqueMaizAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdArranqueMaizAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdArranqueMaizAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ObtenerArranqueMaizPorIdAcond(request.ArranqueMaizId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
