using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelChecklistCommand : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }

        public string Orden { get; set; } = string.Empty;
    }

    public class InsertGranelChecklistCommandHandler : IRequestHandler<InsertGranelChecklistCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelChecklistCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelChecklistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _uow.CreateChecklistGranel(request.EnvasadoraId, request.Orden);

                if (res == 0) return StatusResponse.False(CommandConst.MSJ_INSERT_ERROR);

                //var _arranque = new ArranqueGranel()
                //{
                //    EnvasadoraId = request.EnvasadoraId,
                //    OrdenId = request.Orden,
                //    Cerrado = false,
                //    UsuarioCreacion = _uow.UserName,
                //    FechaCreacion = DateTime.Now,
                //    EsEliminado = false,
                //};

                //await _context.ENV_ArranqueGranels.AddAsync(_arranque);
                //await _context.SaveChangesAsync(cancellationToken);

                //var especificaciones = _context.ENV_ParametroGenerals
                //                               .Where(f => f.PadreId == Convert.ToInt32(ENV_eParametros.ESP_CTRL_GRNL) && f.Activo == true)
                //                               .ToList();

                //var _arranqueEspecificaciones = especificaciones
                //                                    .Select(f => new ArranqueGranelEspecificacion()
                //                                    {
                //                                        ArranqueGranelId = _arranque.ArranqueGranelId,
                //                                        EspecificacionId = f.ParametroGeneralId,
                //                                        Valor = null,
                //                                        Descripcion = "",
                //                                        UsuarioCreacion = _uow.UserName,
                //                                        FechaCreacion = DateTime.Now,
                //                                        EsEliminado = false
                //                                    })
                //                                    .ToList();

                //await _context.ENV_ArranqueGranelEspecificacions.AddRangeAsync(_arranqueEspecificaciones);
                //await _context.SaveChangesAsync(cancellationToken);

                return StatusResponse.True(CommandConst.MSJ_INSERT_OK, data:  res);
            }
            catch (Exception ex)
            {
                var _response = StatusResponse.False(CommandConst.MSJ_INSERT_ERROR);
                _response.AddMessage(ex.Message);
                return _response;
            }
        }
    }
}
