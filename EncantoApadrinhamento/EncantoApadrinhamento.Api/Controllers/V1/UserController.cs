using EncantoApadrinhamento.Core.Enums;
using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.Pagination;
using EncantoApadrinhamento.Domain.RequestModel;
using EncantoApadrinhamento.Domain.ResponseModel;
using EncantoApadrinhamento.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncantoApadrinhamento.Api.Controllers.V1
{
    [ApiController]
    [Authorize(Policy = "Administrator")]
    [Route("api/v1/user")]
    [Produces("application/json")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResult<UserResponse>>> GetAllAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var retorno = await _userService.GetUsersAsync(paginationRequest, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponse>> GetAsync(string userId)
        {
            var retorno = await _userService.GetUserByIdAsync(userId, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserResponse>> GetByEmail(string email)
        {
            var retorno = await _userService.GetUserByEmailAsync(email, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequest createUser)
        {
            if (createUser.UserRole == Enums.Role.Owner)
                return BadRequest("this role is not allowed to be created");

            var retorno = await _userService.CreateUserAsync(createUser, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UserEntity user)
        {
            var retorno = await _userService.UpdateUserAsync(user, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(string userId)
        {
            var retorno = await _userService.DeleteUserAsync(userId, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("role")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> AddUserToRoleAsync(string userId, string role)
        {
            await _userService.AddUserToRoleAsync(userId, role, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok();
        }

        [HttpDelete]
        [Route("role")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> RemoveFromRole(string userId, string role)
        {
            await _userService.RemoveUserFromRoleAsync(userId, role, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("{userId}/roles")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<IList<string>>> GetRoles(string userId)
        {
            var retorno = await _userService.GetUserRolesAsync(userId, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(retorno);
        }
    }
}
