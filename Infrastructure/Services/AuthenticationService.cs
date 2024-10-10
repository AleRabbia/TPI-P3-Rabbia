using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Interfaces;
using Application.Models.Requests;
using Domain.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }
        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.GetByMail(authenticationRequest.Email);
            
            if (user != null && user.Password == authenticationRequest.Password)
            {
                return user;
            }

            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        public string Autenticar(AuthenticationRequest authenticationRequest)
        {
            //Paso 1: Validamos las credenciales
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("thisisthesecretforgeneratingakey(mustbeatleast32bitlong)")); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
           var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()), // ID del usuario
                new Claim("email", user.Email), // Email del usuario
                new Claim("role", user.Role.ToString()), // Rol del usuario
                new Claim("name", user.Name.ToString()),
                new Claim("enabled", user.Enabled.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();


        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public required string Issuer { get; set; }
            public required string Audience { get; set; }
            public required string SecretForKey { get; set; }
        }

    } 

}
