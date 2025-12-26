using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelChecklistRevisionCommand: IRequest<StatusResponse>
    {
        public int ArranqueGranelId { get; set; }
    }

    public class InsertGranelChecklistRevisionCommandHandler : IRequestHandler<InsertGranelChecklistRevisionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelChecklistRevisionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelChecklistRevisionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _uow.GuardarChecklistRevisionGranel(request.ArranqueGranelId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
