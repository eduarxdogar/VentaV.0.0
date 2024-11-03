using WsVenta.Models.Request;
using WsVenta.Models.Response;

namespace WsVenta.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
