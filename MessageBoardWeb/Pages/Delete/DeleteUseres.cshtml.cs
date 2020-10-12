using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages.Delete
{
    public class DeleteUseresModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<DeleteUseresModel> _logger;
        public DeleteUseresModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<DeleteUseresModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }


        public Users User { get; set; }

        public List<Messages> MessagesList { get; set; }

        public void OnGet(int userid)
        {
            User = _usersRepository.GetUser(userid);

        }

        public IActionResult OnPost(int userid)
        {
            _usersRepository.DeleteUser(userid);
            MessagesList = _messagesRepository.getMessageFromUser(userid);
            foreach (Messages messages in MessagesList)
            {
                _messagesRepository.DeleteMessage(messages.MessageId);
            }
            return RedirectToPage("../Index");
        }
    }
}
