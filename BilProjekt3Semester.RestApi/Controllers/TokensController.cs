using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilProjekt3Semester.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private IUserRepo repository;
        private IAuthHelper authenticationHelper;

        public TokensController(IUserRepo repos, IAuthHelper authService)
        {
            repository = repos;
            authenticationHelper = authService;
        }


        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

    }
}