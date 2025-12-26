using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertControlTratamientoPefTiempoAcondCommand : ControlPefTiempoAcondCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertControlTratamientoPefTiempoAcondCommandHandler : IRequestHandler<InsertControlTratamientoPefTiempoAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertControlTratamientoPefTiempoAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertControlTratamientoPefTiempoAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarControlPefTiempoAcond(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
