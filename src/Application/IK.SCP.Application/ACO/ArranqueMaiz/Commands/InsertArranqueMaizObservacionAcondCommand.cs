using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertArranqueMaizObservacionAcondCommand : IRequest<StatusResponse>
    {
        public int ArranqueMaizId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertArranqueMaizObservacionAcondCommandHandler : IRequestHandler<InsertArranqueMaizObservacionAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueMaizObservacionAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueMaizObservacionAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueMaizObservacionAcond(request.ArranqueMaizId, request.Observacion);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
