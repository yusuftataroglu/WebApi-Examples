using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NumberGuessingGameWebApi.DTOs;

namespace NumberGuessingGameWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };

                var result = await _userManager.CreateAsync(identityUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Kayıt başarılı");
                }
                else
                {
                    return BadRequest("Kayıt başarısız");
                }
            }
            else
            {
                return BadRequest("Bilgileri kontrol edin");
            }
        }
    }
}
