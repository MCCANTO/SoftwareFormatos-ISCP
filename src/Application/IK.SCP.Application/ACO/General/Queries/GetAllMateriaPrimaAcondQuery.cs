using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetAllMateriaPrimaAcondQuery : IRequest<StatusResponse>
    {
    }
    public class GetAllMateriaPrimaAcondQueryHandler : IRequestHandler<GetAllMateriaPrimaAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllMateriaPrimaAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllMateriaPrimaAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarMateriaPrimaAcond();
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
