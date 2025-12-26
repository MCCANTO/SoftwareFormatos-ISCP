using IK.SCP.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TimeZoneConverter;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;

        public UnitOfWork(
            IHttpContextAccessor httpContextAccessor,
            Context context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Context Context { get { return _context; } }

        public string UserName
        {
            get
            {
                string userName = "SYSTEM";

                var user = _httpContextAccessor.HttpContext.User;

                if (user == null)
                {
                    return userName;
                }

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return userName;
                }

                var claimName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (claimName == null)
                {
                    return userName;
                }

                userName = claimName.Value.ToUpper();

                return userName;
            }
        }

        public int Perfil
        {
            get
            {
                int perfil = 0;

                var user = _httpContextAccessor.HttpContext.User;

                if (user == null)
                {
                    return perfil;
                }

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return perfil;
                }

                var claimName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (claimName == null)
                {
                    return perfil;
                }

                perfil = Convert.ToInt32(claimName.Value.ToUpper());

                return perfil;
            }
        }

        public DateTime DateTime
        {
            get
            {
                var info = TZConvert.GetTimeZoneInfo("SA Pacific Standard Time");
                DateTime FechaHoy = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                return FechaHoy;
            }
        }

    }
}
