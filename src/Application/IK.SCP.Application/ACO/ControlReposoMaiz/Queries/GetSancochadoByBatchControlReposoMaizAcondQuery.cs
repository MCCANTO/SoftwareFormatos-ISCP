using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetSancochadoByBatchControlReposoMaizAcondQuery : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
        public int NumeroBatch { get; set; }
    }

    public class GetSancochadoByBatchControlReposoMaizAcondQueryHandler : IRequestHandler<GetSancochadoByBatchControlReposoMaizAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetSancochadoByBatchControlReposoMaizAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetSancochadoByBatchControlReposoMaizAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ObtenerDataSancochadoControlReposoMaizAcond(request.OrdenId, request.NumeroBatch);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
