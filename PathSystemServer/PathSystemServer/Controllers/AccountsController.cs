using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Extensions;
using PathSystemServer.Services.Auth;
using PathSystemServer.ViewModels.Auth;

namespace PathSystemServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public ActionResult<CurrentUserViewModel> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reg = _userService.Register(model);
            SetTokenCookie(reg.AccessToken, reg.RefreshToken);

            return Ok(_mapper.Map<CurrentUserViewModel>(reg));
        }

        [HttpPost("Login")]
        public ActionResult<CurrentUserViewModel> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var log = _userService.Login(model);
            SetTokenCookie(log.AccessToken, log.RefreshToken);

            return Ok(_mapper.Map<CurrentUserViewModel>(log));
        }

        [HttpPost("update-access-token")]
        public ActionResult<CurrentUserViewModel> UpdateAccessToken()
        {
            if (!Request.Cookies.TryGetValue("accessToken", out var requestAccessToken) ||
                !Request.Cookies.TryGetValue("refreshToken", out var requestRefreshToken))
            {
                return BadRequest("No tokens provided");
            }

            var accessToken = new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
            var tokens = _userService.UpdateAccessToken(accessToken, requestRefreshToken);

            SetTokenCookie(tokens.AccessToken, tokens.RefreshToken);

            return Ok(_mapper.Map<CurrentUserViewModel>(tokens));
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult RevokeRefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            _userService.RevokeRefreshToken(refreshToken);

            return Ok();
        }

        [HttpPost("exists")]
        public ActionResult<bool> EmailExists(ExistsViewModel model)
        {
            return Ok(_userService.EmailExists(model.Email));
        }

        [Authorize]
        [HttpGet("current")]
        public ActionResult<CurrentUserViewModel> GetCurrentUser()
        {
            var owner = _userService.GetUserFromToken(Request.GetAccessToken());

            return Ok(_mapper.Map<CurrentUserViewModel>(owner));
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
                _userService.RevokeRefreshToken(currentRefreshToken);
        }
    }
}
