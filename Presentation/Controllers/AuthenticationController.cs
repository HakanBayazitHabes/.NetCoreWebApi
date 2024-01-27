using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]//Gelen Dtonun boş olup olmadığını sorguluyor(400 dönüyor).
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var result = await _service.AuthenticationService.RegisterUser(userForRegistrationDto);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]//Gelen Dtonun boş olup olmadığını sorguluyor(400 dönüyor).
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var isUserValid = await _service.AuthenticationService.ValidateUser(userForAuthenticationDto);

        if (!isUserValid)
            return Unauthorized(); // 401

        var tokenDto = await _service.AuthenticationService.CreateToken(true);

        return Ok(tokenDto);
    }

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]//Gelen Dtonun boş olup olmadığını sorguluyor(400 dönüyor).
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

        return Ok(tokenDtoToReturn);
    } 

}
