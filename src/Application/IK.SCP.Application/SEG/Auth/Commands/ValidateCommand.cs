using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.SEG.Auth.Queries;
using IK.SCP.Application.ViewModels;
using IK.SCP.Externo;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace IK.SCP.Application.SEG.Auth.Commands
{
    public class ValidateCommand : IRequest<LoginResponse>
    {
        public string usuario { get; set; }
        public string clave { get; set; }
    }

    public class ValidateCommandHandler : IRequestHandler<ValidateCommand, LoginResponse>
    {
        private readonly IAutorizacionService _autorizacionService;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        public ValidateCommandHandler(IAutorizacionService autorizacionService, IConfiguration config, IMediator mediator)
        {
            _autorizacionService = autorizacionService;
            _config = config;
            _mediator = mediator;
        }

        public async Task<LoginResponse> Handle(ValidateCommand request, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("LLEGUE AL METODO QUE CONSULTA CONTRA EL PROCEDURE PARA REVISAR LA VALIDACION!");
            try
            {
                var credencial = await _autorizacionService.ValidarUsuario(request.usuario, request.clave);

                
                if (credencial == null)
                {
                    return new LoginResponse { Ok = false, Message = "Credenciales incorrectas." };
                }

                var codApp = _config["AppSettings:_CODAPP"]?.ToString();
                var rol = await _autorizacionService.ObtenerRol(codApp ?? "", request.usuario);

                if (rol == null)
                {
                    return new LoginResponse { Ok = false, Message = "Usuario no cuenta con rol asignado." };
                }

                var nodos = await _autorizacionService.ListarNodosXUsuario(codApp ?? "", request.usuario);

                var acciones = (await _mediator.Send(new GetAccionesXRolQuery() { RolId = rol.RolId })).Data;

                var key = _config["AppSettings:EncryptionKey"]?.ToString();

                var _authHelper = new AuthHelper();
                var token = _authHelper.GenerateToken(credencial, rol, nodos, acciones, key);

                return new LoginResponse { Ok = true, Token = token, Message = "Usuario correcto." };
            }
            catch (Exception ex)
            {
                return new LoginResponse { Ok = false, Message = "Error al autenticar usuario." };
            }
        }
    }
}
