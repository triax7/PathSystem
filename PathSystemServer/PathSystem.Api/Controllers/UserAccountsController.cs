using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathSystem.Api.ViewModels.Auth;
using PathSystem.BLL.Commands.Auth.Users;
using PathSystem.BLL.Queries.Auth.Users;

namespace PathSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserAccountsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserViewModel>> Register([FromBody] RegisterUserCommand model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _mediator.Send(model);
            SetTokenCookie(user.AccessToken, user.RefreshToken);

            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] LoginUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _mediator.Send(command);
            SetTokenCookie(user.AccessToken, user.RefreshToken);

            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("update-access-token")]
        public async Task<ActionResult<UserViewModel>> UpdateAccessToken()
        {
            if (!Request.Cookies.TryGetValue("accessToken", out var requestAccessToken) ||
                !Request.Cookies.TryGetValue("refreshToken", out var requestRefreshToken))
            {
                return BadRequest("No tokens provided");
            }

            var userWithTokens = await _mediator.Send(new UpdateUserAccessTokenCommand(requestAccessToken, requestRefreshToken));

            SetTokenCookie(userWithTokens.AccessToken, userWithTokens.RefreshToken);

            return Ok(_mapper.Map<UserViewModel>(userWithTokens));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                await _mediator.Send(new RevokeUserRefreshTokenCommand(refreshToken));

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok();
        }

        [HttpPost("exists")]
        public async Task<ActionResult<bool>> EmailExists([FromBody] UserEmailExistsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserViewModel>> GetCurrentUser()
        {
            var owner = await _mediator.Send(new CurrentUserQuery());

            return Ok(_mapper.Map<UserViewModel>(owner));
        }

        private void SetTokenCookie(string accessToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("accessToken", accessToken, cookieOptions);
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            //revoke current refresh token when we get a new one
            if (Request.Cookies.TryGetValue("refreshToken", out var currentRefreshToken))
                _mediator.Send(new RevokeUserRefreshTokenCommand(currentRefreshToken));
        }
    }
}
