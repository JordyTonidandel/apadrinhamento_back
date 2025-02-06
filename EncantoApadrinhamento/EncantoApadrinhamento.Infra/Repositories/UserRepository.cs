using EncantoApadrinhamento.Core.Enums;
using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.Pagination;
using EncantoApadrinhamento.Domain.ResponseModel;
using EncantoApadrinhamento.Infra.Extensions;
using EncantoApadrinhamento.Infra.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EncantoApadrinhamento.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<UserEntity> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserResponse> GetByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email!,
                    Name = u.Name,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate!.Value,
                    Cpf = u.Cpf,
                    LastName = u.LastName,
                    NickName = u.UserName
                })
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return user ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .AsNoTracking()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email!,
                    Name = u.Name,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate!.Value,
                    Cpf = u.Cpf,
                    LastName = u.LastName,
                    NickName = u.UserName
                })
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken) ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<List<UserResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .AsNoTracking()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email!,
                    Name = u.Name,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate!.Value,
                    Cpf = u.Cpf,
                    LastName = u.LastName,
                    NickName = u.UserName
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<PaginationResult<UserResponse>> GetPagedAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken)
        {
            var paginatedUsers = await _userManager.Users
                .AsNoTracking()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email!,
                    Name = u.Name,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate!.Value,
                    Cpf = u.Cpf,
                    LastName = u.LastName,
                    NickName = u.UserName
                })
                .OrderByDescending(u => u.Id)
                .GetPagedAsync(paginationRequest.Page, paginationRequest.PageSize, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return paginatedUsers;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException("User not found");

            if (await _userManager.IsInRoleAsync(user, "Owner"))
                throw new InvalidOperationException("User cannot be deleted");

            return await _userManager.DeleteAsync(user);
        }

        public async Task AddToRoleAsync(string userId, string role, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to add user to role");
        }

        public async Task RemoveFromRoleAsync(string userId, string role, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to remove user from role");
        }

        public async Task<IList<string>> GetRolesAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return await _userManager.GetRolesAsync(user);
        }
    }
}
