using System;
using Dapper;
using System.Data;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using MediatR;
using IK.SCP.Infrastructure;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllVariableBasicaQuery : IRequest<StatusResponse<List<VariableBasicaViewModel>>>
	{
        public int TipoId { get; set; }
    }

    public class GetAllVariableBasicaQueryHandler : IRequestHandler<GetAllVariableBasicaQuery, StatusResponse<List<VariableBasicaViewModel>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllVariableBasicaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<VariableBasicaViewModel>>> Handle(GetAllVariableBasicaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<VariableBasicaViewModel>("env.PA_LISTAR_VARIABLE_BASICA", new { p_TipoId = request.TipoId }, commandType: CommandType.StoredProcedure);

                items.ToList().ForEach(p =>
                {
                    p.valor = "P";
                });

                return new StatusResponse<List<VariableBasicaViewModel>>()
                {
                    Ok = true,
                    Data = items.OrderBy(o => o.PrimerOrden).ThenBy(o => o.SegundoOrden).ToList()
                };
            }
        }
    }
}

