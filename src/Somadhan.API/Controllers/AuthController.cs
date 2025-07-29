using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Somadhan.API.Models;
using Somadhan.Infrastructure.Identity;

using Swashbuckle.AspNetCore.Annotations;


namespace Somadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterDto model)
    //{
    //    var user = new ApplicationUser
    //    {
    //        UserName = model.Email,
    //        Email = model.Email,
    //        ShopId = model.ShopId
    //    };
    //    var result = await _userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //        return BadRequest(result.Errors);

    //    await _userManager.AddToRoleAsync(user, "Member");
    //    return Ok();
    //}

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "User Login", Description = "Authenticate user and return JWT token.")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim("ShopId", user.ShopId ?? Guid.Empty.ToString())
        };

        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var keyString = _config["Jwt:Key"] ?? "gE29u1kJqv1r09ZlXQ45aB67pL8zR5Nd"; // Fixed path here
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            // Optionally add issuer and audience here if you validate them
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            token_type = "Bearer",
            expires_in = 7200 // 2 hours in seconds
        });
    }


}
