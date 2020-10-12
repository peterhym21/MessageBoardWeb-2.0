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
                messagesList.Add(new Messages { MessageId = (int)myReader["MessageId"], Title = (string)myReader["Title"], Content = (string)myReader["Content"], CategoryId = (int)myReader["CategoryId"], Dato = (DateTime)myReader["Dato"], UserId = (int)myReader["UserId"] });
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
                messagesList.Add(new Messages { MessageId = (int)myReader["MessageId"], Title = (string)myReader["Title"], Content = (string)myReader["Content"], CategoryId = (int)myReader["CategoryId"], Dato = (DateTime)myReader["Dato"], UserId = (int)myReader["UserId"] });
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
                messagesList.Add(new Messages { MessageId = (int)myReader["MessageId"], Title = (string)myReader["Title"], Content = (string)myReader["Content"], CategoryId = (int)myReader["CategoryId"], Dato = (DateTime)myReader["Dato"], UserId = (int)myReader["UserId"] });
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
                if ((int)myReader["UserId"] == messageId)
                {
                    getmessage.Title = (string)myReader["Title"];
                    getmessage.MessageId = (int)myReader["MessageId"];
                    getmessage.Content = (string)myReader["Content"];
                    getmessage.CategoryId = (int)myReader["CategoryId"];
                    getmessage.Dato = (DateTime)myReader["Dato"];
                    getmessage.UserId = (int)myReader["UserId"];
                }
            }
            con.Close();
            return getmessage;
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

            con.Open();
            SqlDataReader Reader = cmd.ExecuteReader();
            Reader.Read();
            messageId = Reader["MessageId"]
            con.Close();
            messagesList = ReadMessages();
            return messageId;
        }


        public int UpdateMessage()
        {
            throw new NotImplementedException();
        }

        public int DeleteMessage()
        {
            throw new NotImplementedException();
        }


    }
}
