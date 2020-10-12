using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages
{
    public class CreateMessageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<CreateMessageModel> _logger;
        public int CategoryId { get; set; }
        public List<Messages> MessagesList { get; set; }
        public List<Category> Categorys { get; set; }
        public int messageId { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty]
        public string Category { get; set; }

        public Users User { get; set; }

        public CreateMessageModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<CreateMessageModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }


        public void OnGet(int userid)
        {
            User = _usersRepository.GetUser(userid);
        }


        public IActionResult OnPost(int userid)
        {
            if (Category != null)
            {
                CategoryId = _categoryRepos.CreateCategory(Category);
            }
            if (Category == null)
            {
                CategoryId = 1;
            }
            if (userid == 0)
                messageId = _messagesRepository.CreateMessages(Title, Content, CategoryId, 1);
            else
                messageId = _messagesRepository.CreateMessages(Title, Content, CategoryId, userid);

            return RedirectToPage("Details/DetaileMessages", new { id = messageId });

        }
    }
}
