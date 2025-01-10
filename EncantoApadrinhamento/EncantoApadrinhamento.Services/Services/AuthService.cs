using EncantoApadrinhamento.Core.CustomException;
using EncantoApadrinhamento.Core.Util;
using EncantoApadrinhamento.Services.Interfaces;
using EncantoApadrinhamento.Services.RequestModel.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EncantoApadrinhamento.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginRequest loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email).ConfigureAwait(false) ?? throw new DomainException("Usuário não encontrado");

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
                throw new DomainException("Usuário bloqueado, por favor tente mais tarde!");

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password).ConfigureAwait(false);

            if (passwordIsValid)
            {
                if (!user.EmailConfirmed)
                    throw new DomainException("Email do usuário não confirmado");

                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                if (!userRoles.Any())
                    throw new DomainException("Nenhuma role encontrada para este usuário");

                return _tokenService.GenerateToken(user, userRoles);
            }
            else
            {
                user.AccessFailedCount++;

                if (user.AccessFailedCount >= 5)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now.AddMinutes(10);
                    user.AccessFailedCount = 0;
                }

                await _userManager.UpdateAsync(user).ConfigureAwait(false);

                throw new DomainException("Senha inválida");
            }
        }

        public async Task<bool> ConfirmEmailAsync(Guid id, string confirmToken)
        {
            var user = await _userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false) ?? throw new DomainException("Usuário não encontrado");

            if (user.EmailConfirmed)
                throw new DomainException("Email do usuário já confirmado");

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmToken));

            var result = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

            if (result.Succeeded)
                return true;
            else
                throw new DomainException("Erro ao confirmar email favor contatar o administrador do sistema");
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email).ConfigureAwait(false);

            if (user == null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"{_configuration["Base:Url"]}/api/v1/auth/reset-password/{user.Id}?token={code}";

            await _emailService.SendEmailAsync(user.Email!, "Recuperação de senha", TemplatesEmail.RecuperacaoSenha(user.UserName!, confirmationLink), true).ConfigureAwait(false);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest resetPasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDTO.Id.ToString()).ConfigureAwait(false) ?? throw new DomainException("Usuário não encontrado");

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDTO.Token));

            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.NewPassword).ConfigureAwait(false);

            if (!result.Succeeded)
                throw new DomainException("Erro ao resetar senha favor contatar o administrador do sistema");

            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now).ConfigureAwait(false);
            await _userManager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);
            await _userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);
            await _userManager.UpdateAsync(user).ConfigureAwait(false);
            await _emailService.SendEmailAsync(user.Email!, "Senha alterada", TemplatesEmail.SenhaAlterada(user.UserName!), true).ConfigureAwait(false);
        }

        public async Task<string> RefreshTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false) ?? throw new DomainException("Usuário não encontrado");

            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            return _tokenService.GenerateToken(user, userRoles);
        }
    }
}
