using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateBlendingArranqueCierreCommand : IRequest<StatusResponse>
    {
        public int BlendingArranqueId { get; set; }
    }

    public class UpdateBlendingArranqueCierreCommandHandler : IRequestHandler<UpdateBlendingArranqueCierreCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateBlendingArranqueCierreCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateBlendingArranqueCierreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.CerrarArranqueBlending(request.BlendingArranqueId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
