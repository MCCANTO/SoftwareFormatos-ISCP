using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertBlendingArranqueCommand : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
    }
    public class InsertBlendingArranqueCommandHandler : IRequestHandler<InsertBlendingArranqueCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertBlendingArranqueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertBlendingArranqueCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueBlending(request.Orden);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
