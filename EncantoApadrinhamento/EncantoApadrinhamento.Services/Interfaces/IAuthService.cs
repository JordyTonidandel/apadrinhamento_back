using EncantoApadrinhamento.Domain.RequestModel.Auth;

namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest loginDTO);
        Task<bool> ConfirmEmailAsync(Guid id, string confirmToken);
        Task ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordDTO);
        Task ResetPasswordAsync(ResetPasswordRequest resetPasswordDTO);
        Task<string> RefreshTokenAsync(string userId);
    }
}
