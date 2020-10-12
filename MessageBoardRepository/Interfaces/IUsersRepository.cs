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
        int CreateUseres(string UserName, string Password);
        int UpdateCategory(string Username, string Password, int Userid);
        int DeleteCategory(int Userid);
    }
}
