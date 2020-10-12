using MessageBoardRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Interfaces
{
    public interface IUsersRepository
    {
        List<Users> GetUsers();
        Users GetUserlogin(string username, string password );
        Users GetUser(int userId);
        int CreateUseres(string UserName, string Password);
        int UpdateUser(string Username, string Password, int Userid);
        void DeleteUser(int Userid);
    }
}
