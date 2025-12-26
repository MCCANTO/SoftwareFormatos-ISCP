using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllOrdenConsumoQuery : IRequest<StatusResponse<object>>
    {
        public string Orden { get; set; }
        public string? Clasificacion { get; set; }
    }

    public class GetAllOrdenConsumoQueryHandler : IRequestHandler<GetAllOrdenConsumoQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllOrdenConsumoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllOrdenConsumoQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
         
                var parametros = new
                {
                    p_Orden = request.Orden,
                    p_Clasificacion = request.Clasificacion
                };

                var data = await cnn.QueryAsync<dynamic>("FR.LISTAR_SABOR_CONSUMO", parametros, commandType: CommandType.StoredProcedure);

                var items = data.Select(p => new
                {
                    Articulo = p.Articulo,
                    Descripcion = p.Descripcion
                }).ToList();

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items
                };
            }

        }
    }
}
