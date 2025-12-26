using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertControlMaizRemojoAcondCommand: RemojoMaizControlAcondUpdateDto, IRequest<StatusResponse>
    {
    }

    public class InsertControlMaizRemojoAcondCommandHandler : IRequestHandler<InsertControlMaizRemojoAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertControlMaizRemojoAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertControlMaizRemojoAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarControlMaizRemojoAcond(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
