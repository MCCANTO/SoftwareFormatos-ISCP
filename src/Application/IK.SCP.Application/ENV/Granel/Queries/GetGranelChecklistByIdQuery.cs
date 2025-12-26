using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetGranelChecklistByIdQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }

    public class GetGranelChecklistByIdQueryHandler : IRequestHandler<GetGranelChecklistByIdQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetGranelChecklistByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(GetGranelChecklistByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var checklist = await _uow.ObtenerChecklistGranelPorId(request.Id);
                return StatusResponse.TrueFalse((checklist != null),QueryConst.MSJ_GET_OK, QueryConst.MSJ_GET_ERROR, data: checklist);

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
