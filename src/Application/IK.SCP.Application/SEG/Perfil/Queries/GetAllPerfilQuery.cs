using IK.SCP.Infrastructure;
using MediatR;
namespace IK.SCP.Application.SEG.Queries
{
    public class GetAllPerfilQuery : IRequest<List<object>>
	{
	}

    public class GetAllPerfilQueryHandler : IRequestHandler<GetAllPerfilQuery, List<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllPerfilQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<object>> Handle(GetAllPerfilQuery request, CancellationToken cancellationToken)
        {
            //var data = await _uow.Perfiles.GetAllPerfil();
            var data = new List<object>();
            return data;
        }
    }
}

