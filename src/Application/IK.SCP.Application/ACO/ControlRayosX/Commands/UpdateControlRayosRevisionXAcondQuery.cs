using System;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
	public class UpdateControlRayosRevisionXAcondQuery:IRequest<StatusResponse>
	{
		public List<int> Ids { get; set; }
	}

    public class UpdateControlRayosRevisionXAcondQueryHandler : IRequestHandler<UpdateControlRayosRevisionXAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateControlRayosRevisionXAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateControlRayosRevisionXAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.RevisarControlRayosXAcond(request.Ids);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

