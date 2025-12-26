using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertOrdenAcondCommand : OrdenAcondCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertOrdenAcondCommandHandler : IRequestHandler<InsertOrdenAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertOrdenAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertOrdenAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.InsertarOrdenAcond(request);
                return StatusResponse.TrueFalse(result.Item1, CommandConst.MSJ_INSERT_OK, result.Item2);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
