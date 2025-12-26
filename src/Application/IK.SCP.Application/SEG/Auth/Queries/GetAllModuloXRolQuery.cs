using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SEG.Queries
{
    public class GetAllModuloXRolQuery : IRequest<StatusResponse<object>>
    {
        public int RolId { get; set; }
    }

    public class GetAllModuloXRolQueryHandler : IRequestHandler<GetAllModuloXRolQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllModuloXRolQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllModuloXRolQuery request, CancellationToken cancellationToken)
        {
            //var _acciones = _context.SEG_RolAccions
            //                        .Where(f => f.RolId == request.RolId && f.Activo)
            //                        .Include(i => i.Accion)
            //                        .Select(p => p.Accion)
            //                        .ToList();
                
            //var _opciones = _context.SEG_RolOpcions
            //                        .Where(f => f.RolId == request.RolId && f.Activo)
            //                        .Include(i => i.Opcion)
            //                        .Select(p => new
            //                        {
            //                            p.OpcionId,
            //                            p.Opcion.ModuloId,
            //                            p.Opcion.Codigo,
            //                            p.Opcion.Nombre,
            //                            Acciones = _acciones.Count > 0 ? _acciones.Where(f => f.OpcionId == p.OpcionId).ToList() : null,
            //                        }).ToList();

            //var _modulos = _context.SEG_RolModulos
            //                .Where(f => f.RolId == request.RolId && f.Activo)
            //                .Include(i => i.Modulo)
            //                .Select(p => p.Modulo).Distinct().ToList();

            //var _data = _modulos.Select(p => new
            //                {
            //                    p.ModuloId,
            //                    p.Codigo,
            //                    p.Nombre,
            //                    Opciones = _opciones.Count > 0 ? _opciones.Where(f => f.ModuloId == p.ModuloId).ToList() : null,
            //                }).ToList();

            return new StatusResponse<object>()
            {
                Ok = true,
            };
        }
    }
}
