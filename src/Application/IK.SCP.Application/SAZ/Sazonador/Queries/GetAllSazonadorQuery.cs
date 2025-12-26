using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Queries
{
    public class GetAllSazonadorQuery : IRequest<StatusResponse>
    {
    }

    public class GetAllSaborizadorQueryHandler : IRequestHandler<GetAllSazonadorQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllSaborizadorQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllSazonadorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarSazonadores();
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
