using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertControlMaizObservacionAcondCommand : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertControlMaizObservacionAcondCommandHandler : IRequestHandler<InsertControlMaizObservacionAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertControlMaizObservacionAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertControlMaizObservacionAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarObservacionMaizPeladoAcond(request.OrdenId, request.Observacion);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}