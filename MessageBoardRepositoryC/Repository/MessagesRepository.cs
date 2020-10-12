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
    public class MessagesRepository : IMessagesRepository
    {
        private readonly string _conString;
        private List<Messages> messagesList;
        Messages getmessage;

        public MessagesRepository(string conString)
        {
            _conString = conString;
        }


        public List<Messages> ReadMessages()
        {
            messagesList = new List<Messages>();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("ReadMessages", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                messagesList.Add(new Messages
                {
                    MessageId = (int)myReader["MessageId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    CategoryId = (int)myReader["CategoryId"],
                    Dato = (DateTime)myReader["Dato"],
                    UserId = (int)myReader["UserId"]
                });
            }

            con.Close();
            return messagesList;
        }


        public List<Messages> GetTopTen()
        {
            messagesList = new List<Messages>();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("GetTopTenMessages", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                messagesList.Add(new Messages
                {
                    MessageId = (int)myReader["MessageId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    CategoryId = (int)myReader["CategoryId"],
                    Dato = (DateTime)myReader["Dato"],
                    UserId = (int)myReader["UserId"]
                });
            }

            con.Close();
            return messagesList;
        }


        public List<Messages> ReadMessagesByCategory(int categoryId)
        {
            messagesList = new List<Messages>();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("GetCategoryMessage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                messagesList.Add(new Messages
                {
                    MessageId = (int)myReader["MessageId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    CategoryId = (int)myReader["CategoryId"],
                    Dato = (DateTime)myReader["Dato"],
                    UserId = (int)myReader["UserId"]
                });
            }

            con.Close();
            return messagesList;
        }

        public Messages GetMessage(int messageId)
        {
            getmessage = new Messages();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("GetMessage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GetMessageId", messageId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                getmessage.Title = (string)myReader["Title"];
                getmessage.MessageId = (int)myReader["MessageId"];
                getmessage.Content = (string)myReader["Content"];
                getmessage.CategoryId = (int)myReader["CategoryId"];
                getmessage.Dato = (DateTime)myReader["Dato"];
                getmessage.UserId = (int)myReader["UserId"];
            }
            con.Close();
            return getmessage;
        }

        public List<Messages> getMessageFromUser(int userId)
        {
            messagesList = new List<Messages>();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("GetMessageFromUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                messagesList.Add(new Messages
                {
                    Title = (string)myReader["Title"],
                    MessageId = (int)myReader["MessageId"],
                    Content = (string)myReader["Content"],
                    CategoryId = (int)myReader["CategoryId"],
                    Dato = (DateTime)myReader["Dato"],
                    UserId = (int)myReader["UserId"]
                });

            }
            con.Close();
            return messagesList;
        }

        public int CreateMessages(string Title, string Content, int CategoryId, int UserId)
        {
            int messageId = 0;

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("CreateMessage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Content", Content);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);

            SqlParameter param = new SqlParameter("@MessageId", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            messageId = Convert.ToInt32(cmd.Parameters["@MessageId"].Value);
            return messageId;
        }

        public void DeleteMessage(int messageId, int userid)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("DeleteMessage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MessageId", messageId);
            cmd.Parameters.AddWithValue("@UserId", userid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public int UpdateMessage(int messageId, string title, string content, int categoryId)
        {

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("UpdateMessage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MessageId", messageId);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return messageId;
        }


    }
}
