using MessageBoardRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Interfaces
{
    public interface IMessagesRepository
    {
        List<Messages> ReadMessages();
        List<Messages> GetTopTen();
        List<Messages> ReadMessagesByCategory(int categoryId);
        Messages GetMessage(int messageId);
        int CreateMessages(string title, string content, int categoryId, int userId);
        int UpdateMessage();
        int DeleteMessage();
    }
}
