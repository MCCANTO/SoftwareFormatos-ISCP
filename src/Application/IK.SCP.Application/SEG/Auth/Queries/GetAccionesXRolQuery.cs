using Dapper;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.SEG.Auth.Queries
{
    public class GetAccionesXRolQuery : IRequest<StatusResponse>
    {
        public int RolId { get; set; }
    }

    public class GetAccionesXRolQueryHandler : IRequestHandler<GetAccionesXRolQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAccionesXRolQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAccionesXRolQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (var cnn = this._uow.Context.CreateConnection)
                {
                    var parametros = new
                    {
                        p_RolId = request.RolId,
                    };

                    var result = await cnn.QueryAsync<dynamic>("SEG.LISTAR_ACCIONES_X_ROL", parametros, commandType: CommandType.StoredProcedure);

                    return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
                }
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
            
        }
    }
}
