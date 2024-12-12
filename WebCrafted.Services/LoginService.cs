using WebCrafted.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebCrafted.Services
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }
        
        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false; 
            }
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }
        public async Task<bool> CreateUser(string email, string password, string firstName, string lastName)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return false; 
            }

            var newUser = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = _passwordHasher.HashPassword(null, password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}