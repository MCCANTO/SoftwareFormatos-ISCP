using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.Queries
{
    public class OpcionesXPerfilQuery : IRequest<List<object>>
    {
    }

    public class OpcionesXPerfilQueryHandler : IRequestHandler<OpcionesXPerfilQuery, List<object>>
    {
        private readonly IUnitOfWork _uow;

        public OpcionesXPerfilQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<object>> Handle(OpcionesXPerfilQuery request, CancellationToken cancellationToken)
        {
            var response = new List<object>();
            //try
            //{
            //    var items = await _uow.Perfiles.GetOpcionesXPerfil(_uow.Perfil);
            //    return items;
            //}
            //catch (Exception ex)
            //{
            //    response = new List<OpcionDto>();
            //    Console.WriteLine(ex.Message);
            //}

            return response;
        }
    }
}
