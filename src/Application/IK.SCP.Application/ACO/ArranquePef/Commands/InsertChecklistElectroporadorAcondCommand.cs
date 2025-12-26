using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertChecklistElectroporadorAcondCommand : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }
    public class InsertChecklistElectroporadorAcondCommandHandler : IRequestHandler<InsertChecklistElectroporadorAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertChecklistElectroporadorAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertChecklistElectroporadorAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.InsertarArranqueElectroporadorAcond(request.OrdenId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}