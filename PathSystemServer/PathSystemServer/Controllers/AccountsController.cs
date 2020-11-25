﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathSystemServer.DTOs.Auth;
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
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reg = _userService.Register(_mapper.Map<RegisterDTO>(model));
            SetTokenCookie(reg.AccessToken, reg.RefreshToken);

            return Ok(_mapper.Map<LoginSuccessViewModel>(reg));
        }

        [HttpPost("Login")]
        public ActionResult<LoginSuccessViewModel> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var log = _userService.Login(_mapper.Map<LoginDTO>(model));
            SetTokenCookie(log.AccessToken, log.RefreshToken);

            return Ok(_mapper.Map<LoginSuccessViewModel>(log));
        }

        [HttpPost("update-access-token")]
        public IActionResult UpdateAccessToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var requestAccessToken = Request.Cookies["accessToken"];

            if (refreshToken == null || requestAccessToken == null) return Unauthorized();
            
            var accessToken = new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
            var tokens = _userService.UpdateAccessToken(accessToken, refreshToken);

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

            _userService.RevokeRefreshToken(refreshToken);

            return Ok();
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
