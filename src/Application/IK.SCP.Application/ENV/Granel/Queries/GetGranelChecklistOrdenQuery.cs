using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetGranelChecklistOrdenQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
    }

    public class GetGranelChecklistOrdenQueryHandler : IRequestHandler<GetGranelChecklistOrdenQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetGranelChecklistOrdenQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetGranelChecklistOrdenQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var checklist = await _uow.ObtenerChecklistGranel(request.EnvasadoraId, request.Orden);

                if (checklist == null) return StatusResponse.False("No existe Checklist");

                return StatusResponse.True("Consulta exitosa", data: checklist);

            }
            catch (Exception ex)
            {
                var _response = StatusResponse.False("Error al consultar la información.");
                _response.AddMessage(ex.Message);
                return _response;
            }
        }
    }
}
