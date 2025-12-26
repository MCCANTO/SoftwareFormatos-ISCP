using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IK.SCP.Application.SAZ.Queries
{
    public class GetAllSazonadorLineaFRQuery : IRequest<StatusResponse>
    {
        public int SaborizadorId { get; set; }
    }

    public class GetAllSaborizadoLineaFRQueryHandler : IRequestHandler<GetAllSazonadorLineaFRQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllSaborizadoLineaFRQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllSazonadorLineaFRQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarFreidorasXSazonador(request.SaborizadorId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}