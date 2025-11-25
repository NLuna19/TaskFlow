using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // TODO: Replace SHA256 with BCrypt or ASP.NET Identity password hasher.
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            // Domain validation
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");
            // (Email es obligatorio)

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username is required.");
            // (Username es obligatorio)

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Password is required.");
            // (Password es obligatorio)

            // Validate unique email
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (emailExists)
                throw new InvalidOperationException("Email already exists.");
            // (Ya existe un usuario con ese email)

            // Hash password
            user.PasswordHash = HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null)
                return false;

            // Domain validation
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username is required.");

            // Hash password if provided
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                user.PasswordHash = HashPassword(user.PasswordHash);
            else
                user.PasswordHash = existing.PasswordHash;

            // Check unique email except self
            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == user.Email && u.Id != user.Id);

            if (emailExists)
                throw new InvalidOperationException("Email already exists.");

            // Copy updated values
            _context.Entry(existing).CurrentValues.SetValues(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
