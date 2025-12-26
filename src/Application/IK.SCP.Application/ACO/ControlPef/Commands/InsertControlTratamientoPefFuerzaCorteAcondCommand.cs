using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertControlTratamientoPefFuerzaCorteAcondCommand : ControlPefFuerzaCorteAcondCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertControlTratamientoPefFuerzaCorteAcondCommandHandler : IRequestHandler<InsertControlTratamientoPefFuerzaCorteAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertControlTratamientoPefFuerzaCorteAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertControlTratamientoPefFuerzaCorteAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarControlPefFuerzaCorteAcond(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}