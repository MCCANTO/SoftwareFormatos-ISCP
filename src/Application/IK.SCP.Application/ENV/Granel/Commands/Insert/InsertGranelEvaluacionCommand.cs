using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelEvaluacionCommand : EvaluactionPTCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertGranelEvaluacionCommandHandler : IRequestHandler<InsertGranelEvaluacionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelEvaluacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelEvaluacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _uow.GuardarEvaluacionPTGranel(request);

                return StatusResponse.TrueFalse(res, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                var _response = StatusResponse.False(CommandConst.MSJ_INSERT_ERROR);
                _response.AddMessage(ex.Message);
                return _response;
            }
        }
    }
}
