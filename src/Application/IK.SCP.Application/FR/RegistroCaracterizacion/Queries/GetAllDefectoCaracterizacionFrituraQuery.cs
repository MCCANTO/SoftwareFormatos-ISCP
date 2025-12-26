using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllDefectoCaracterizacionFrituraQuery : IRequest<StatusResponse>
    {
        public string Articulo { get; set; }
    }
    public class GetAllDefectoCaracterizacionFrituraQueryHandler : IRequestHandler<GetAllDefectoCaracterizacionFrituraQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllDefectoCaracterizacionFrituraQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(GetAllDefectoCaracterizacionFrituraQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarDefectoCaracterizacion(request.Articulo);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
