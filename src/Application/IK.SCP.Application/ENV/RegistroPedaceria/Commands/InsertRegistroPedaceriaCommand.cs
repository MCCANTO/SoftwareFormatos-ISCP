using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertRegistroPedaceriaCommand : EnvasadoRegistroPedaceriaCreateDto, IRequest<StatusResponse>
    {}

    public class InsertRegistroPedaceriaCommandHandler : IRequestHandler<InsertRegistroPedaceriaCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public InsertRegistroPedaceriaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertRegistroPedaceriaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarEnvasadoRegistroPedaceria(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}

