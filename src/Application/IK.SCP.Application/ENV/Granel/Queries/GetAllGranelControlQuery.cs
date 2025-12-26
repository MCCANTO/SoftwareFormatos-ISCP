using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllGranelControlQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
    }

    public class GetAllGranelControlQueryHandler : IRequestHandler<GetAllGranelControlQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllGranelControlQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllGranelControlQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarControlGranel(request.EnvasadoraId, request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
