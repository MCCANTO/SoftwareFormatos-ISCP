using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class InsertArranqueObservacionSaborizadoCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertArranqueObservacionSaborizadoCommandHandler : IRequestHandler<InsertArranqueObservacionSaborizadoCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueObservacionSaborizadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(InsertArranqueObservacionSaborizadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var _arranqueObservacion = new ArranqueObservacion()
                //{
                //    ArranqueId = request.ArranqueId,
                //    Observacion = request.Observacion,
                //    UsuarioCreacion = _uow.UserName,
                //    FechaCreacion = DateTime.Now,
                //    EsEliminado = false
                //};

                //await _context.SAZ_ArranqueObservacions.AddAsync(_arranqueObservacion);
                //await _context.SaveChangesAsync(cancellationToken);

                return new StatusResponse<int> { Ok = true, Message = "Información guardada correctamente", Data = 1 };
            }
            catch (Exception ex)
            {
                return new StatusResponse<int> { Ok = false, Message = "No se pudo guardar la información", Messages = new string[] { ex.Message }, Data = 0 };
            }
        }
    }
}
