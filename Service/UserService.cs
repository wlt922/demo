using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public ReusltApiMsg GetUser(string name, string password)
        {
            return _repository.GetUser(name, password);
        }

        public ReusltApiMsg GetUsers()
        {
            return _repository.GetUsers();
        }

        public string hello()
        {
            return _repository.hello();
        }
    }
}
