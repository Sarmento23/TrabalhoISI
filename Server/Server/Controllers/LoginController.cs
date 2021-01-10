using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Objects;
using Server.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DBManagement _context;

        public LoginController(DBManagement context)
        {
            _context = context;
        }


        [HttpPost]
        
        public async Task<ActionResult<Object>> Authenticate([FromBody]Login model)
        {
            // Recupera o usuário
            var user = _context.users.FirstOrDefault(c => c.Username.Equals(model.Username) && c.Password.Equals(model.Password));

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
