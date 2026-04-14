using FixIt.Core.Features.Auth.DTOs;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly FIXITDbContext _context;
        private readonly JWTService _JwtService;
        private readonly IService<User> _UserService;
        private readonly IService<WorkerProfile> _WorkerService;
        private readonly IService<Wallet> _WalletService;
        public AccountController( 
            FIXITDbContext context ,
            JWTService JwtService, 
            IService<User> UserService , 
            IService<WorkerProfile> WorkerService , 
            IService<Wallet> WalletService)
        {
            _context= context;
            _JwtService = JwtService;
            _UserService = UserService;
            _WorkerService = WorkerService;
            _WalletService = WalletService;
        }











        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userData)
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
                Role = userData.Role.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userData.Password)
            };
            await _UserService.AddAsync(user);
            if(userData.Role.ToLower() == "worker")
            {
                var worker = new WorkerProfile() { UserId = user.UserId };
                await _WorkerService.AddAsync(worker);
            }
            var wallet = new Wallet() 
            {
                UserId = user.UserId ,
                OwnerType = user.Role
            };
            await _WalletService.AddAsync(wallet);

            string StringToken = _JwtService.GenerateToken(user);

            return Ok(new { token = StringToken });
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

            if(userFromDb == null)
            {
                ModelState.AddModelError("", "لا يوجد حساب بهذا البريد الإلكتروني أو نوع الحساب غير صحيح.");
                return BadRequest(ModelState);
            }
            bool validPassword = BCrypt.Net.BCrypt.Verify(userFromRequst.Password, userFromDb.PasswordHash);
            if (userFromDb == null || !validPassword)
            {
                ModelState.AddModelError("","الايميل او كلمة المرور غير صحيحه");
                return BadRequest(ModelState);
            }

            string StringToken = _JwtService.GenerateToken(userFromDb);

            return Ok(new { token = StringToken });
        }
    }
}
