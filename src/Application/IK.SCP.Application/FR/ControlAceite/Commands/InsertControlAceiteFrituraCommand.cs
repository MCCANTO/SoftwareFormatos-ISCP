using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class InsertControlAceiteFrituraCommand : ControlAceiteCreateDto, IRequest<StatusResponse>
    { }

    public class InsertControlAceiteFrituraCommandHandler : IRequestHandler<InsertControlAceiteFrituraCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public InsertControlAceiteFrituraCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(InsertControlAceiteFrituraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarControlAceite(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}