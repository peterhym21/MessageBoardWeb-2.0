using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages.Details
{
    public class DetaileMessagesModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<DetaileMessagesModel> _logger;
        public DetaileMessagesModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<DetaileMessagesModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty]
        public string Category { get; set; }

        public Messages GetMessages { get; set; }

        public Category GetCategory { get; set; }
        public Users User { get; set; }

        public void OnGet(int id)
        {
            GetMessages = _messagesRepository.GetMessage(id);
            GetCategory = _categoryRepos.ReadOneCategories(GetMessages.CategoryId);
            User = _usersRepository.GetUser(GetMessages.UserId);
        }
    }
}
