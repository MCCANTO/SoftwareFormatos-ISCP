using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetGranelParametrosControlQuery : IRequest<StatusResponse>
    {
    }

    public class GetGranelParametrosControlQueryHandler : IRequestHandler<GetGranelParametrosControlQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetGranelParametrosControlQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetGranelParametrosControlQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarParametroControlGranel();
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
