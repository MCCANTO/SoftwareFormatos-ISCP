using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllParametroGeneralQuery: IRequest<StatusResponse<List<ParametroGeneralViewModel>>>
	{
		public int padreId { get; set; }
	}

    public class GetAllParametroGeneralQueryHandler : IRequestHandler<GetAllParametroGeneralQuery, StatusResponse<List<ParametroGeneralViewModel>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllParametroGeneralQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<ParametroGeneralViewModel>>> Handle(GetAllParametroGeneralQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<ParametroGeneralViewModel>("env.PA_LISTAR_PARAMETRO_GENERAL_X_PADRE_ID", new { p_PadreId = request.padreId }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<ParametroGeneralViewModel>>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}

