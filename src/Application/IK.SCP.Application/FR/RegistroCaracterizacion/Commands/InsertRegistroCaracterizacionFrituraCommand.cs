using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class InsertRegistroCaracterizacionFrituraCommand : RegistroCaracterizacionCreateDto, IRequest<StatusResponse>
    {
    }
    public class InsertRegistroCaracterizacionFrituraCommandHandler : IRequestHandler<InsertRegistroCaracterizacionFrituraCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public InsertRegistroCaracterizacionFrituraCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(InsertRegistroCaracterizacionFrituraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarRegistroCaracterizacion(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
