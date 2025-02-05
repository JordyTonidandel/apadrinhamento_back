using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.Pagination;
using EncantoApadrinhamento.Domain.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Infra.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponse?> GetByIdAsync(string userId, CancellationToken cancellationToken);
        Task<UserResponse?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<UserResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<PaginationResult<UserResponse>> GetPagedAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistsByIdAsync(string userId, CancellationToken cancellationToken);

        Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(string userId, CancellationToken cancellationToken);

        Task AddToRoleAsync(string userId, string role, CancellationToken cancellationToken);
        Task RemoveFromRoleAsync(string userId, string role, CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(string userId, CancellationToken cancellationToken);
    }
}
