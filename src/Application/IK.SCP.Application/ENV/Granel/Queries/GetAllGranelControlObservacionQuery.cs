using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllGranelControlObservacionQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
    }

    public class GetAllGranelControlObservacionQueryHandler : IRequestHandler<GetAllGranelControlObservacionQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllGranelControlObservacionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllGranelControlObservacionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarObservacionControlGranel(request.EnvasadoraId, request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
