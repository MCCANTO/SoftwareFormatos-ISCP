using System;
using Dapper;
using System.Data;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using MediatR;
using IK.SCP.Infrastructure;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllCondicionesPreviasQuery : IRequest<StatusResponse<object>>
	{
        public int tipoId { get; set; }
        public int linea { get; set; }
    }

    public class GetAllCondicionesPreviasQueryHandler : IRequestHandler<GetAllCondicionesPreviasQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllCondicionesPreviasQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllCondicionesPreviasQuery request, CancellationToken cancellationToken)
        {

            using (var cnn = _uow.Context.CreateConnection)
            {
                var parametros = new { p_TipoId = request.tipoId, p_Linea = request.linea };

                var items = await cnn.QueryAsync<dynamic>("FR.LISTAR_CONDICIONES_PREVIAS", parametros, commandType: CommandType.StoredProcedure);

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }

    }
}

