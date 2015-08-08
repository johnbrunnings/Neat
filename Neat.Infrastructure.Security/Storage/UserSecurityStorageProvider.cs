using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public class UserSecurityStorageProvider : IUserSecurityStorageProvider
    {
        private readonly IStorageApplication<User> _userStorageApplication;

        public UserSecurityStorageProvider(IStorageApplication<User> userStorageApplication)
        {
            _userStorageApplication = userStorageApplication;
        }

        public IQueryable<User> GetAll()
        {
            return _userStorageApplication.GetAll();
        }

        public User GetById(string id)
        {
            return _userStorageApplication.GetById(id);
        }

        public User Add(User entity)
        {
            return _userStorageApplication.Add(entity);
        }

        public void Update(User entity)
        {
            _userStorageApplication.Update(entity);
        }

        public void Delete(User entity)
        {
            _userStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _userStorageApplication.Delete(id);
        }
    }
}