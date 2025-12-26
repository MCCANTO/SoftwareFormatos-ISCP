using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Queries
{
    public class GetAllArranqueVerificacionSazonadoQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }

    public class GetAllArranqueVerificacionSazonadoQueryHandler : IRequestHandler<GetAllArranqueVerificacionSazonadoQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllArranqueVerificacionSazonadoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllArranqueVerificacionSazonadoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarArranqueVerificacionSazonado(request.Id);

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
