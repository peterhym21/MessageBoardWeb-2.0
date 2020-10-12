using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Repository
{
    public class UserRepository : IUsersRepository
    {
        private readonly string _conString;
        private List<Users> UserList;

        public UserRepository(string conString)
        {
            _conString = conString;
        }
        public List<Users> GetUsers()
        {
            throw new NotImplementedException();
        }

        public int CreateUseres(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public int UpdateCategory(string Username, string Password, int Userid)
        {
            throw new NotImplementedException();
        }

        public int DeleteCategory(int Userid)
        {
            throw new NotImplementedException();
        }
    }
}
