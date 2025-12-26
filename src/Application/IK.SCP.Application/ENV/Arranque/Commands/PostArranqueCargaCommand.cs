
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueCargaCommand : IRequest<StatusResponse>
    {
        public IFormCollection archivos { get; set; }
        public eTipoCarga tipo { get; set; }
    }

    public class PostArranqueCargaCommandHandler : IRequestHandler<PostArranqueCargaCommand, StatusResponse>
    {
        private readonly IConfiguration _config;
        private readonly ILogger<PostArranqueCargaCommandHandler> _logger;

        public PostArranqueCargaCommandHandler(IConfiguration config, ILogger<PostArranqueCargaCommandHandler> logger)
        {
            _config = config;
            _logger = logger;
        }


        public async Task<StatusResponse> Handle(PostArranqueCargaCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.archivos.Files.Any()) return StatusResponse.False("No existe archivo a adjuntar.");
                

                var file = request.archivos.Files[0];

                var nombreNuevo = Guid.NewGuid().ToString() + "." + file.FileName.Split(".")[1]; ;
                
                string path = "";
                switch (request.tipo)
                {
                    case eTipoCarga.ENV_CODIFICACION:
                        path = Path.Combine(_config.GetSection("RutasCarga:CargaCodificacionEnvasado").Value ?? "", nombreNuevo);
                        break;
                    case eTipoCarga.ENV_INSPECCION:
                        path = Path.Combine(_config.GetSection("RutasCarga:CargaInspeccionEnvasado").Value ?? "", nombreNuevo);
                        break;
                    case eTipoCarga.ENV_GRANEL_CODIFICACION:
                        path = Path.Combine(_config.GetSection("RutasCarga:CargaInspeccionEnvasadoGranel").Value ?? "", nombreNuevo);
                        break;
                    default:
                        break;
                }
                _logger.LogInformation(path);
                
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var data = new
                {
                    nombre = nombreNuevo,
                    ruta = path
                };

                return StatusResponse.True(CommandConst.MSJ_INSERT_OK, data: data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
