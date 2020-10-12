using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Entities
{
    public class Messages
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Dato { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

    }
}
