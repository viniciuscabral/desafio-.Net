using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsProjetoVinicius.Controllers;
using WsProjetoVinicius.Model;
using WsTestes.Model;


namespace WsTestes.Controllers
{
    
    [ApiController]
    public class UserController : ControllerBase
    {             
        private IUserService _userService;       
        public UserController(IUserService userService)
        {            
            _userService = userService;           
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetUsers() //apenas para teste e recuperação de token.
        {
            return Ok(_userService.GetAllUser()); //exibe tudo 
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult AddUser(User user)
        {            
            UserDto userRet = _userService.AddUser(user);
            return CreatedAtAction("AddUser", new { msg = "User successfully registered." }); // se tudo ok
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Sigin(User user)
        {
            return Ok(_userService.SingIn(user));           
        }

        [HttpPost]
        [Route("me")]
        public ActionResult AutenticationUser()
        {            
            string token = "";
            Microsoft.Extensions.Primitives.StringValues headerValues;
            var nameFilter = string.Empty;
            if (HttpContext.Request.Headers.TryGetValue("Autentication", out headerValues))
            {
                token = headerValues.ToString();
            }
            return Ok(_userService.AutorizationUser(token));   
        }


    }
}
