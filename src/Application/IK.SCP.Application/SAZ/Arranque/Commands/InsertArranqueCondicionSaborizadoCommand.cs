using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.SAZ.ViewModels;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class InsertArranqueCondicionSaborizadoCommand : ArranqueSazonadoCondicionUpdateDto, IRequest<StatusResponse>
    {
    }

    public class InsertArranqueCondicionSaborizadoCommandHandler : IRequestHandler<InsertArranqueCondicionSaborizadoCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueCondicionSaborizadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueCondicionSaborizadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueCondicionSazonado(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }

            
        }
    }
}
