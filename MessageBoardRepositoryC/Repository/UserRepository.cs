using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Repository
{
    public class UserRepository : IUsersRepository
    {
        #region Feltes and Ctor

        private readonly string _conString;
        private List<Users> userList;
        private Users user;

        public UserRepository(string conString)
        {
            _conString = conString;
        }

        #endregion

        public List<Users> GetUsers()
        {
            userList = new List<Users>();

            //SQL connection
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("ReadUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                userList.Add(new Users { UserId = (int)myReader["UseriId"], UserName = (string)myReader["UserName"], Password = (string)myReader["Password"] });
            }

            con.Close();
            return userList;
        }


        public Users GetUserlogin(string username, string password)
        {
            user = new Users();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("ReadOneUserwithPass", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                user.UserId = (int)myReader["UserId"];
                user.UserName = (string)myReader["UserName"]; 
                user.Password = (string)myReader["Password"];
            }

            con.Close();
            return user;
        }


        public Users GetUser(int userId)
        {
            user = new Users();
            //SQL connection
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("ReadOneUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                user.UserId = (int)myReader["UserId"];
                user.UserName = (string)myReader["UserName"];
                user.Password = (string)myReader["Password"];
            }

            con.Close();
            return user;
        }


        public int CreateUseres(string UserName, string Password)
        {
            int userid = 0;

            //SQL connection
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("CreateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlParameter param = new SqlParameter("@UserId", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            userid = Convert.ToInt32(cmd.Parameters["@UserId"].Value);
            return userid;
        }


        public int UpdateUser(string Username, string Password, int Userid)
        {
            //SQL connection
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("UpdateUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@updateUsername", Username);
            cmd.Parameters.AddWithValue("@UpdatePassword", Password);
            cmd.Parameters.AddWithValue("@UserId", Userid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return Userid;
        }


        public void DeleteUser(int Userid)
        {
            //SQL connection
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("DeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", Userid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}
