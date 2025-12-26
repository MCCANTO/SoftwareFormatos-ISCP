using System;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
	public class GetAllProcesoMateriaPrimaAcondQuery : IRequest<StatusResponse>
    {
        public int MateriaPrimaId { get; set; }
    }

    public class GetAllProcesoMateriaPrimaAcondQueryHandler : IRequestHandler<GetAllProcesoMateriaPrimaAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllProcesoMateriaPrimaAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllProcesoMateriaPrimaAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarProcesoXMateriaPrimaAcond(request.MateriaPrimaId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

