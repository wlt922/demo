using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        public UserRepository(DbContext dbContext)
        { 
            _dbContext = dbContext;
        }
        
        public string hello()
        {
           
            return "Hello from UserRepository";
        }

        public ReusltApiMsg GetUsers()
        {
            var users = _dbContext.Db.Queryable<User>("SELECT * FROM Users").ToList();
            _dbContext.Db.Queryable<User>().Where(it => it.Id == 1).First();
            _dbContext.Db.Queryable<User>().Where(it => it.Id > 1 && it.Id < 5 && it.username.Contains("a")).ToList();
            var exp = Expressionable.Create<User>();
            var userName = "";
            var password = "";
            var where = exp.And(it => it.username.Contains("a"))
                .OrIF(!string.IsNullOrEmpty(userName), it => it.username == userName)
                .AndIF(!string.IsNullOrEmpty(userName), it => it.password == password)
                .ToExpression();
            int[] ids = { 1, 2, 3 };
            _dbContext.Db.Queryable<User>().AS("t1").Where(s=>ids.Contains(s.Id)).ToList();
            var users2 = _dbContext.Db.Queryable<User>().Where(where);

            return new ReusltApiMsg { Success = true, Message = "", Data = users };
        }

        public ReusltApiMsg GetUser(string name, string password)
        {
            var user = _dbContext.Db.Queryable<User>().Single(it => it.username == name && it.password == password);
            if (user == null)
            {
                return new ReusltApiMsg { Success = false, Message = "User not found", Data = null };
            }
            return new ReusltApiMsg { Success = true, Message = "", Data = user };
        }
    }
}
