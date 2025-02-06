using EncantoApadrinhamento.Core.Enums;
using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.Pagination;
using EncantoApadrinhamento.Domain.RequestModel;
using EncantoApadrinhamento.Domain.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResult<UserResponse>> GetUsersAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
        Task<UserResponse> GetUserByIdAsync(string userId, CancellationToken cancellationToken);
        Task<UserResponse> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> UserExistsByIdAsync(string userId, CancellationToken cancellationToken);
        Task<IdentityResult> CreateUserAsync(CreateUserRequest userRequest, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUserAsync(string userId, CancellationToken cancellationToken);
        Task AddUserToRoleAsync(string userId, string role, CancellationToken cancellationToken);
        Task RemoveUserFromRoleAsync(string userId, string role, CancellationToken cancellationToken);
        Task<IList<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken);
        Task<List<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
