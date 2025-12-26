using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands;

public class SaveArranqueEnvasadoCommand : ArranqueEnvasadoDto, IRequest<StatusResponse<int>>
{ }

public class SaveArranqueEnvasadoCommandHandler : IRequestHandler<SaveArranqueEnvasadoCommand, StatusResponse<int>>
{
    private readonly IUnitOfWork _uow;

    public SaveArranqueEnvasadoCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse<int>> Handle(SaveArranqueEnvasadoCommand request, CancellationToken cancellationToken)
    {

        using (var cnn = _uow.Context.CreateConnection)
        {
            cnn.Open();
            try
            {
                var id = await _uow.GuardarEnvasadoArranqueEnvasado(request);
               
                if (id == 0)
                    return new StatusResponse<int> { Ok = false, Message = "No se pudo guardar la información" };
                else
                    return new StatusResponse<int> { Ok = true, Data = id, Message = "Información guardada correctamente" };

            }
            catch (Exception ex)
            {
                return new StatusResponse<int> { Ok = false };
            }
        }
    }
}

