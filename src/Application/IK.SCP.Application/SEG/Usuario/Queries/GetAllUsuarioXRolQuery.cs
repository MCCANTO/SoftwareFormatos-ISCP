using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SEG.Queries
{
    public class GetAllUsuarioXRolQuery : IRequest<StatusResponse>
    {
        public int RolId { get; set; }
    }

    public class GetAllUsuarioXRolQueryHandler : IRequestHandler<GetAllUsuarioXRolQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllUsuarioXRolQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllUsuarioXRolQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
