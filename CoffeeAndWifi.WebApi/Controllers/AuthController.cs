using CoffeeAndWifi.WebApi.Models;
using CoffeeAndWifi.WebApi.Models.Converters;
using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyFinances.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeAndWifi.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="unitOfWork"> UnitOfWork </param>
        public AuthController(UnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userDtoRequest"> UserDto </param>
        /// <returns>string UserName</returns>
        [HttpPost("Register")]
        public DataResponse<string> Register(UserDto userDtoRequest)
        {
            var response = new DataResponse<string>();
            try
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDtoRequest.Password);
                var user = userDtoRequest.ToDao();
                user.PasswordHash = passwordHash;
                user.RoleId = 1;
                _unitOfWork.User.Add(user);
                _unitOfWork.Complete();
                response.Data = user.Username;
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Login standard User
        /// </summary>
        /// <param name="userDtoRequest"> UserDto - userName and password</param>
        /// <returns>string UserName </returns>
        [HttpPost("Login")]
        public DataResponse<string> Login(UserDto userDtoRequest)
        {
            var response = new DataResponse<string>();
            try
            {
                userDtoRequest.RoleId = 1;
                _unitOfWork.User.Verify(userDtoRequest);
                response.Data = userDtoRequest.Username;
                response.Jwt = CreateToken(userDtoRequest.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Login as Admin
        /// </summary>
        /// <param name="userDtoRequest"> UserDto - userName and password</param>
        /// <returns>string Admin userName </returns>
        [HttpPost("AdminLogin")]
        public DataResponse<string> AdminLogin(UserDto userDtoRequest)
        {
            var response = new DataResponse<string>();
            try
            {
                userDtoRequest.RoleId = 2;
                _unitOfWork.User.Verify(userDtoRequest);
                response.Data = userDtoRequest.Username;
                response.Jwt = CreateToken(userDtoRequest.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        /// <summary>
        /// Delete USer
        /// </summary>
        /// <param name="id"> int User.Id</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public Response Delete(int id)
        {
            var response = new Response();
            try
            {
                _unitOfWork.User.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }


    }
}
