using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetGranelCondicionOperativaDetalleQuery : IRequest<StatusResponse>
	{
		public int Id { get; set; }
	}

    public class GetGranelCondicionOperativaDetalleQueryHandler : IRequestHandler<GetGranelCondicionOperativaDetalleQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetGranelCondicionOperativaDetalleQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetGranelCondicionOperativaDetalleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarCondicionOperativaGranelDetalle(request.Id);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

