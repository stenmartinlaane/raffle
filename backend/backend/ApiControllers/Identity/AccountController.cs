using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0.Identity;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using WebApp.DTO;

namespace WebApp.ApiControllers.Identity;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private Random Random = new Random();
    private readonly int JwtCookieExpireTimeInMinutes = 1;

    public AccountController(UserManager<AppUser> userManager, ILogger<AccountController> logger,
        SignInManager<AppUser> signInManager, IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [Produces("application/json")]
    public async Task<ActionResult> Login([FromBody] LoginInfo loginInfo)
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            await Task.Delay(Random.Next(1, 101));
            return NotFound("User/Password problem");
        }

        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            await Task.Delay(Random.Next(1, 101));
            return NotFound("User/Password problem");
        }

        var deletedRows = await _context.RefreshTokens
            .Where(t => t.AppUserId == appUser.Id && t.ExpirationDT < DateTime.UtcNow)
            .ExecuteDeleteAsync();
        _logger.LogInformation("Deleted {} refresh tokens", deletedRows);

        var refreshToken = new AppRefreshToken()
        {
            AppUserId = appUser.Id
        };
        refreshToken.AppUser = appUser;
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        // new ClaimsPrincipal(new ClaimsIdentity(claimsPrincipal.Claims, "MyCookieScheme"));
        //
        // Console.WriteLine("claims values here");
        // foreach (var claim in claimsPrincipal.Claims)
        // {
        //     Console.WriteLine(claim);
        // }

        await HttpContext.SignInAsync("MyCookieScheme", claimsPrincipal,
            new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(JwtCookieExpireTimeInMinutes),
            }
        );
        
        Response.Cookies.Append("refreshToken", refreshToken.RefreshToken, new CookieOptions
        {
            Secure = false,
            HttpOnly = true,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(90),
            IsEssential = true,
            Path = "/api/v1/identity/Account/"
        });
        
        Response.Cookies.Append("refreshTokenTimer", "", new CookieOptions
        {
            Secure = false,
            HttpOnly = false,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(90),
            IsEssential = true,
            Path = "/api/v1/identity/Account/"
        });

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [Produces("application/json")]
    public async Task<ActionResult> RefreshJwt()
    {
        // validate refresh token
        var cookierefreshToken = Request.Cookies
            .Where(c => c.Key == "refreshToken")
            .Select(c => c.Value)
            .FirstOrDefault();
        
        
        var refreshToken = _context.RefreshTokens.AsQueryable().Include(rt => rt.AppUser).FirstOrDefault(token => token.RefreshToken == cookierefreshToken);
        if (refreshToken == null)
        {
            return BadRequest("Invalid refreshtoken");
        }
        
        // get user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(refreshToken.AppUser);
        

        // genereate jwt
        await HttpContext.SignInAsync("MyCookieScheme", claimsPrincipal,
            new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(JwtCookieExpireTimeInMinutes),
            }
        );
        
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [Produces("application/json")]
    public async Task<ActionResult> Register([FromBody] RegisterInfo registerInfo)
    {
        var user = new AppUser()
        {
            Email = registerInfo.Email,
            UserName = registerInfo.Password,
            FirstName = registerInfo.Firstname,
            LastName = registerInfo.Lastname
        };
        var res = _userManager.CreateAsync(user, registerInfo.Password).Result;
        
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> LogOut()
    {
        await HttpContext.SignOutAsync("MyCookieScheme",
            new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(90)
            }
        );
        
        var refreshToken = Request.Cookies
            .Where(c => c.Key == "refreshToken")
            .Select(c => c.Value)
            .FirstOrDefault();
        if (refreshToken == null)
        {
            return BadRequest();
        }
        
        var deletedRows = await _context.RefreshTokens
            .Where(t => t.RefreshToken == refreshToken)
            .ExecuteDeleteAsync();
        _logger.LogInformation("Deleted {} refresh tokens", deletedRows);


        
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            Secure = false,
            HttpOnly = true,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
            IsEssential = true,
            Path = "/api/v1/identity/Account/"
        });
        
        Response.Cookies.Append("refreshTokenTimer", "", new CookieOptions
        {
            Secure = false,
            HttpOnly = false,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
            IsEssential = true,
            Path = "/api/v1/identity/Account/"
        });
        
        return Ok();
    }
}