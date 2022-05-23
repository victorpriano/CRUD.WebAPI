using CRUD.WebAPI.Data;
using CRUD.WebAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var user = await _user.GetAll();

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter os usuários." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            try
            {
                _user.AddUser(user);

                await _user.SaveChangesAsync();
                return Ok(user);

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível adicionar um novo usuário." }); // Trocar por Created
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            try
            {
                if(user.Id != id)
                {
                    return NotFound($"Este User não existe!");
                }

                _user.UpdateUser(user);

                await _user.SaveChangesAsync();
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível atualizar o usuário!" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                var user = await _user.GetByUser(id);

                if(user == null)
                {
                    return NotFound($"Este usuário não existe!");
                }

                _user.DeleteUser(user);

                await _user.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível deletar este usuário." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _user.GetByUser(id);

            return Ok(user);
        }
    }
}
