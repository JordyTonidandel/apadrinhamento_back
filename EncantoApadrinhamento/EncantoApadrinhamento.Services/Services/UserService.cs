using EncantoApadrinhamento.Core.Enums;
using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.Pagination;
using EncantoApadrinhamento.Domain.RequestModel;
using EncantoApadrinhamento.Domain.ResponseModel;
using EncantoApadrinhamento.Infra.Interfaces;
using EncantoApadrinhamento.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginationResult<UserResponse>> GetUsersAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetPagedAsync(paginationRequest, cancellationToken);

            return users;
        }

        public async Task<UserResponse> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            return user ?? throw new KeyNotFoundException();
        }

        public async Task<UserResponse> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            return user ?? throw new KeyNotFoundException();
        }

        public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userRepository.ExistsByEmailAsync(email, cancellationToken);
        }

        public async Task<bool> UserExistsByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userRepository.ExistsByIdAsync(userId, cancellationToken);
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserRequest userRequest, CancellationToken cancellationToken)
        {
            var user = new UserEntity
            {
                Name = userRequest.Name,
                LastName = userRequest.LastName,
                BirthDate = userRequest.BirthDate,
                Cpf = userRequest.Cpf,
                Email = userRequest.Email,
                UserName = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber
            };

            var userCreated = await _userRepository.CreateAsync(user, cancellationToken);

            await _userRepository.AddToRoleAsync(user.Id, userRequest.UserRole.ToString(), cancellationToken);

            return userCreated;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteAsync(userId, cancellationToken);
        }

        public async Task AddUserToRoleAsync(string userId, string role, CancellationToken cancellationToken)
        {
            await _userRepository.AddToRoleAsync(userId, role, cancellationToken);
        }

        public async Task RemoveUserFromRoleAsync(string userId, string role, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveFromRoleAsync(userId, role, cancellationToken);
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userRepository.GetRolesAsync(userId, cancellationToken);
        }

        public async Task<List<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
