using System;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
	public class GetAllControlRayosXAcondQuery : IRequest<StatusResponse>
	{
		public string Periodo { get; set; }
	}

    public class GetAllControlRayosXAcondQueryHandler : IRequestHandler<GetAllControlRayosXAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllControlRayosXAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllControlRayosXAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarControlRayosXAcond(request.Periodo);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

