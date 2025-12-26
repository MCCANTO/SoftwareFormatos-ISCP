using System;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
	public class GetChecklistEspecificacionesQuery : IRequest<StatusResponse>
	{
		public int ArranqueGranelId { get; set; }
	}

    public class GetChecklistEspecificacionesQueryHanlder : IRequestHandler<GetChecklistEspecificacionesQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetChecklistEspecificacionesQueryHanlder(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetChecklistEspecificacionesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarChecklistGranelEspecificaciones(request.ArranqueGranelId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

