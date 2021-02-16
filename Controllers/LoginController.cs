using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Services;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    [Route("userlogin")]
    public class UserloginController : Controller
    {
            [HttpPost]
            [Route("")]

            public async Task<ActionResult<dynamic>>  Authenticate(
                [FromServices] DataContext context,
                [FromBody] User model)
            {
                var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();

                if ( user == null)
                    return NotFound( new {message = "Usuário ou Senha Inválido"});

                var token = TokenService.GenerateToken(user);
                return new
                {
                    user = user,
                    token = token
                };
         }

         [HttpGet]
         [Route("anonimo")]
         [AllowAnonymous]

         public string Anonimo() => "Anonimo";

        [HttpGet]
        [Route("autenticado")]
        [Authorize]

         public string Autenticado() => "Autenticado";
    }
}