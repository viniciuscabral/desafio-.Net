using System.Collections.Generic;
using System.Threading.Tasks;
using WsProjetoVinicius.Model;
using WsTestes.Model;

namespace WsProjetoVinicius.Controllers
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        UserDto AddUser(User user);
        UserDto SingIn(User user);
        UserDto AutorizationUser(string autentication);
    }
}
