﻿using EncantoApadrinhamento.Domain.Models.RequestModel.Auth;
using EncantoApadrinhamento.Domain.RequestModel.Auth;
using EncantoApadrinhamento.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EncantoApadrinhamento.Api.Controllers.V1.Auth
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest login)
        {
            string? clientIP = HttpContext?.Connection?.RemoteIpAddress?.ToString();

            _logger.LogInformation($"Login: {login.Email} Ip: {clientIP}");

            var token = await _authService.LoginAsync(login).ConfigureAwait(false);

            _logger.LogInformation($"Login: {login.Email} - Token: {token}");

            return Ok(new
            {
                Token = token,
                TokenExpires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:MinutesToExpire"]!)),
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest register)
        {
            await _authService.RegisterAsync(register).ConfigureAwait(false);

            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshTokenAsync()
        {
            var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var token = await _authService.RefreshTokenAsync(userId!).ConfigureAwait(false);

            return Ok(new
            {
                Token = token,
                TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:MinutesToExpire"]!)),
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("confirm-email/{id:guid}")]
        public async Task<ActionResult> ConfirmEmailAsync([FromRoute] Guid id, [FromQuery] string token)
        {
            await _authService.ConfirmEmailAsync(id, token).ConfigureAwait(false);

            return Ok("Email confirmado com sucesso!");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest esqueciMinhaSenhaRequest)
        {
            await _authService.ForgotPasswordAsync(esqueciMinhaSenhaRequest).ConfigureAwait(false);

            return Ok("Email enviado com sucesso!");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetSenhaRequest)
        {
            await _authService.ResetPasswordAsync(resetSenhaRequest).ConfigureAwait(false);

            return Ok("Senha alterada com sucesso!");
        }
    }
}
