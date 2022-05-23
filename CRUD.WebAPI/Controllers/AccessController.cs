using CRUD.WebAPI.Data;
using CRUD.WebAPI.DataAccess.Repositories;
using CRUD.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUser _user;

        public AccessController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] UserLogin user)
        {
            var userExists = _user.GetByUsersAuthenticate(user.UserName, user.Password);

            if(userExists == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }
            else
            {
                var token = TokenService.GenerateToken(user);
                return new
                {
                    token = token
                };
            }
        }
    }
}
