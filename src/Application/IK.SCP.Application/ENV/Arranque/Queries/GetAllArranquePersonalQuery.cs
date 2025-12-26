using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranquePersonalQuery : IRequest<object>
    {
        public int ArranqueId { get; set; }
    }

    public class GetAllArranquePersonalQueryHandler : IRequestHandler<GetAllArranquePersonalQuery, object>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranquePersonalQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<object> Handle(GetAllArranquePersonalQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var data = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_PERSONAL", new { p_ArranqueId = request.ArranqueId }, commandType: CommandType.StoredProcedure);

                var items = data
                                .GroupBy(x => new { x.NroGrupo, x.UsuarioCreacion, x.FechaCreacion })
                                .Select(x => new
                                {
                                    Item = x.Key.NroGrupo,
                                    Usuario = x.Key.UsuarioCreacion,
                                    Fecha = x.Key.FechaCreacion,
                                    Empacadores = x.Where(f => f.CargoId == "EMPACADOR").Select(p => p.Nombre),
                                    Paletizadores = x.Where(f => f.CargoId == "PALETIZADOR").Select(p => p.Nombre),
                                })
                                .OrderBy(o => o.Item)
                                .ToList();


                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items
                };
            }
        }
    }
}