﻿using Dem.Application.Abstraction.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dem.Infrastracture.Token;

public class TokenHandler : ITokenHandler
{
    readonly IConfiguration _configuration;
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Application.Model.Token CreateAccessToken()
    {
        Application.Model.Token token = new();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);
        token.Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Token:AccessTokenExpiration"]));
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
            );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
