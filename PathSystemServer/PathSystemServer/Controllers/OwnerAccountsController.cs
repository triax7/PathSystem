using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Services.Auth;
using PathSystemServer.ViewModels.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace PathSystemServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerAccountsController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnerAccountsController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public ActionResult<LoginSuccessViewModel> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reg = _ownerService.Register(_mapper.Map<RegisterDTO>(model));
            SetTokenCookie(reg.AccessToken, reg.RefreshToken);

            return Ok(_mapper.Map<LoginSuccessViewModel>(reg));
        }

        [HttpPost("Login")]
        public ActionResult<LoginSuccessViewModel> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var log = _ownerService.Login(_mapper.Map<LoginDTO>(model));
            SetTokenCookie(log.AccessToken, log.RefreshToken);

            return Ok(_mapper.Map<LoginSuccessViewModel>(log));
        }

        [HttpPost("update-access-token")]
        public ActionResult<LoginSuccessViewModel> UpdateAccessToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var requestAccessToken = Request.Cookies["accessToken"];
            var accessToken = new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
            var tokens = _ownerService.UpdateAccessToken(accessToken, refreshToken);

            SetTokenCookie(tokens.AccessToken, tokens.RefreshToken);

            return Ok(_mapper.Map<LoginSuccessViewModel>(tokens));
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult RevokeRefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            _ownerService.RevokeRefreshToken(refreshToken);

            return Ok();
        }

        [HttpPost("exists")]
        public ActionResult<bool> EmailExists(ExistsViewModel model)
        {
            return Ok(_ownerService.EmailExists(model.Email));
        }

        [Authorize]
        [HttpGet("current")]
        public ActionResult<CurrentUserViewModel> GetCurrentUser()
        {
            var requestAccessToken = Request.Cookies["accessToken"];
            var accessToken = new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
            var owner = _ownerService.GetUserFromToken(accessToken);

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
        }
    }
}
