using Microsoft.Extensions.Configuration;
using ServiceReference;
using static ServiceReference.SoftEmpAccesoClient;

namespace IK.SCP.Externo
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IConfiguration _config;

        public AutorizacionService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Credencial> ValidarUsuario(string usuario, string clave)
        {
            var cliente = new SoftEmpAccesoClient(EndpointConfiguration.BasicHttpBinding_ISoftEmpAcceso, _config["AppSettings:UrlApiSeguridad"]);

            System.Diagnostics.Debug.WriteLine("Quienes son los usuarios que llegarn???");
            System.Diagnostics.Debug.WriteLine("Usuario: " + usuario);
            System.Diagnostics.Debug.WriteLine("Password: " + clave);
            var response = await cliente.ValidaAccesoAsync(usuario, clave, false);

            return response;
        }


        public async Task<Credencial> ObtenerCredencial(string usuario)
        {
            var cliente = new SoftEmpAccesoClient(EndpointConfiguration.BasicHttpBinding_ISoftEmpAcceso, _config["AppSettings:UrlApiSeguridad"]);

            var response = await cliente.ObtenerCredencialAsync(usuario.Trim().ToUpper(), true);

            return response;
        }

        public async Task<Rol> ObtenerRol(string codigoApp, string usuario)
        {
            var cliente = new SoftEmpAccesoClient(EndpointConfiguration.BasicHttpBinding_ISoftEmpAcceso, _config["AppSettings:UrlApiSeguridad"]);

            var response = await cliente.ObtenerRolAsync(codigoApp, usuario);

            return response;
        }

        public async Task<List<Nodo>> ListarNodosXUsuario(string codigoApp, string usuario)
        {
            var cliente = new SoftEmpAccesoClient(EndpointConfiguration.BasicHttpBinding_ISoftEmpAcceso, _config["AppSettings:UrlApiSeguridad"]);
            var response = await cliente.ObtenerNodosAsync(codigoApp, usuario);
            return response.ToList();
        }
    }
}
