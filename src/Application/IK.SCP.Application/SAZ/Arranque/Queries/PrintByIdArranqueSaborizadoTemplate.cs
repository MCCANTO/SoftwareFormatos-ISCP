using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Queries;

public class PrintByIdArranqueSaborizadoTemplate : IRequest<StatusResponse>
{
    public int id { get; set; }
}

public class PrintByIdArranqueSaborizadoTemplateHandler : IRequestHandler<PrintByIdArranqueSaborizadoTemplate, StatusResponse>
{
    private readonly IUnitOfWork _unitOf;

    public PrintByIdArranqueSaborizadoTemplateHandler(IUnitOfWork unitOf)
    {
        _unitOf = unitOf;
    }

    public async Task<StatusResponse> Handle(PrintByIdArranqueSaborizadoTemplate request, CancellationToken cancellationToken)
    {
        return new StatusResponse();
    }
}