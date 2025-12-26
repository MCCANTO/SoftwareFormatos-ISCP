using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetBlendingValidacionArticuloQuery : IRequest<StatusResponse>
    {
        public string Articulo { get; set; }
    }

    public class GetBlendingValidacionArticuloQueryHandler : IRequestHandler<GetBlendingValidacionArticuloQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetBlendingValidacionArticuloQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetBlendingValidacionArticuloQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ValidarArticuloMezclaBlending(request.Articulo);
                return StatusResponse.TrueFalse(result, QueryConst.MSJ_GET_OK, QueryConst.MSJ_GET_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
