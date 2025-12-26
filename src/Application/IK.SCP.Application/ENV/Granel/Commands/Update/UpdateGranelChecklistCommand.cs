using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateGranelChecklistCommand : IRequest<StatusResponse>
    {
        public int ArranqueGranelId { get; set; }
    }

    public class UpdateGranelChecklistCommandHandler : IRequestHandler<UpdateGranelChecklistCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateGranelChecklistCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateGranelChecklistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.CloseChecklistGranel(request.ArranqueGranelId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
