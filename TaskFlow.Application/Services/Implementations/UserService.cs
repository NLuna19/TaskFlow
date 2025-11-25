using TaskFlow.Application.Interfaces.Repositories;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<User>> GetAllAsync()
            => _repo.GetAllAsync();

        public Task<User?> GetByIdAsync(int id)
            => _repo.GetByIdAsync(id);

        public Task<User?> GetByEmailAsync(string email)
            => _repo.GetByEmailAsync(email);

        public Task<User> CreateAsync(User user)
            => _repo.CreateAsync(user);

        public Task<bool> UpdateAsync(User user)
            => _repo.UpdateAsync(user);

        public Task<bool> DeleteAsync(int id)
            => _repo.DeleteAsync(id);
    }
}
