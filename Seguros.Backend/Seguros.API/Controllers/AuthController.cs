using Microsoft.AspNetCore.Mvc;
using Seguros.Application.Services;
using Seguros.Domain.Entities;
using Seguros.Domain.Interfaces.Repositories;

namespace Seguros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUsuarioRepository usuarioRepository, IAuthService authService, ILogger<AuthController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario request)
    {
        var existing = await _usuarioRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            return BadRequest("Email ya registrado");

        request.PasswordHash = _authService.HashPassword(request.PasswordHash);
        await _usuarioRepository.AddAsync(request);

        return Ok("Usuario registrado");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _usuarioRepository.GetByEmailAsync(request.Email);
        if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Intento de login fallido para el email {Email}", request.Email);
            return Unauthorized("El usuario o la contraseña son incorrectos");
        }
        var token = _authService.GenerateJwtToken(user);
        _logger.LogInformation("Login exitoso para el email {Email}", request.Email);
        return Ok(new { Token = token, Usuario = user.NombreUsuario }) ;
    }
}

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}