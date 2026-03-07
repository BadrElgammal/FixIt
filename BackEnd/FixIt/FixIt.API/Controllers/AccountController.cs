using FixIt.Core.Features.Auth.DTOs;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly FIXITDbContext _context;
        private readonly IConfiguration config;
        private readonly IService<User> _UserService;
        private readonly IService<WorkerProfile> _WorkerService;
        private readonly IService<Wallet> _WalletService;
        public AccountController( 
            FIXITDbContext context , 
            IConfiguration config, 
            IService<User> UserService , 
            IService<WorkerProfile> WorkerService , 
            IService<Wallet> WalletService)
        {
            _context= context;
            this.config = config;
            _UserService = UserService;
            _WorkerService = WorkerService;
            _WalletService = WalletService;
        }











        [HttpPost("Register")]
        public IActionResult Register(RegisterDTO userData)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_UserService.Find(u => u.Email == userData.Email).Any())
            {
                ModelState.AddModelError("Email", "هذا البريد الإلكتروني مستخدم بالفعل. يرجى استخدام بريد آخر.");
                return BadRequest(ModelState);
            }
            if (_UserService.Find(u => u.Phone == userData.Phone).Any())
            {
                ModelState.AddModelError("Email", "رقم الهاتف مستخدم بالفعل. يرجى استخدام رقم اخر.");
                return BadRequest(ModelState);
            }
            if (userData.Password != userData.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "كلمة المرور وتأكيد كلمة المرور غير متطابقتين.");
                return BadRequest(ModelState);
            }


            var user = new User
            {
                FullName = userData.Name,
                Email = userData.Email,
                Phone = userData.Phone,
                City = userData.City,
                Role = userData.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userData.Password)
            };
            _UserService.Create(user);
            if(userData.Role.ToLower() == "worker")
            {
                var worker = new WorkerProfile() { UserId = user.UserId };
                _WorkerService.Create(worker);
            }
            var wallet = new Wallet() 
            {
                UserId = user.UserId ,
                OwnerType = user.Role
            };
            _WalletService.Create(wallet);


            return Ok("Created");
        }







        [HttpPost("Login")]
        public IActionResult Login(LoginDTO userFromRequst)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var userFromDb = _context.Users.FirstOrDefault(u => u.Email == userFromRequst.Email && u.PasswordHash == userFromRequst.Password);
            var userFromDb = _UserService.Find(u => u.Email == userFromRequst.Email ).FirstOrDefault();

            bool validPassword = BCrypt.Net.BCrypt.Verify(userFromRequst.Password, userFromDb.PasswordHash);
            if (userFromDb == null || !validPassword)
            {
                ModelState.AddModelError("","الايميل او كلمة المرور غير صحيحه");
                return BadRequest(ModelState);
            }


            #region Claims
            List<Claim> userData = new List<Claim>();
            userData.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userData.Add(new Claim(ClaimTypes.NameIdentifier , userFromDb.UserId.ToString()));
            userData.Add(new Claim(ClaimTypes.Name, userFromDb.FullName));
            userData.Add(new Claim(ClaimTypes.Email, userFromDb.Email));
            userData.Add(new Claim(ClaimTypes.Role, userFromDb.Role));
            #endregion


            #region Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            #endregion

            #region Create Token
            //signingCre
            var signCre = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            //Create Token
            var Token = new JwtSecurityToken(
                audience: config["Jwt:AudienceIP"],
                issuer: config["Jwt:IssuerIP"],
                claims:userData,
                signingCredentials:signCre,
                expires:DateTime.Now.AddDays(1)
                );

            var StringToken = new JwtSecurityTokenHandler().WriteToken(Token);
            #endregion

            return Ok(StringToken);
        }
    }
}
