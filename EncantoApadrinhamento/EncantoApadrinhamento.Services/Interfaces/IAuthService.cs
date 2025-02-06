using EncantoApadrinhamento.Domain.Models.RequestModel.Auth;
using EncantoApadrinhamento.Domain.RequestModel.Auth;
using EncantoApadrinhamento.Domain.ResponseModel;

namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest loginDTO);
        Task<UserResponse> RegisterAsync(RegisterRequest registerDTO);
        Task<bool> ConfirmEmailAsync(Guid id, string confirmToken);
        Task ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordDTO);
        Task ResetPasswordAsync(ResetPasswordRequest resetPasswordDTO);
        Task<string> RefreshTokenAsync(string userId);

    }
}
