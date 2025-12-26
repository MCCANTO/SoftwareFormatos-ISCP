using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueMaquinaObservacionQuery : IRequest<StatusResponse>
    {
        public int arranqueMaquinaId { get; set; }
        public string observacion { get; set; }
    }

    public class GetArranqueMaquinaObservacionQueryHandler : IRequestHandler<GetArranqueMaquinaObservacionQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetArranqueMaquinaObservacionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetArranqueMaquinaObservacionQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
