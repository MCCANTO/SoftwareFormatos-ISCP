using System;
using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllCondicionesPreviasQuery : IRequest<StatusResponse<List<CondicionPreviaViewModel>>>
	{
        public int TipoId { get; set; }
    }

    public class GetAllCondicionesPreviasQueryHandler : IRequestHandler<GetAllCondicionesPreviasQuery, StatusResponse<List<CondicionPreviaViewModel>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllCondicionesPreviasQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<CondicionPreviaViewModel>>> Handle(GetAllCondicionesPreviasQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<CondicionPreviaViewModel>("env.PA_LISTAR_CONDICION_PREVIA", new { p_TipoId = request.TipoId }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<CondicionPreviaViewModel>>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}

