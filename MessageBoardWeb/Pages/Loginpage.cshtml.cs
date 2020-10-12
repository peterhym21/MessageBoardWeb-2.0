using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages
{
    public class LoginpageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<LoginpageModel> _logger;

        public LoginpageModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<LoginpageModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public Users User { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            User = _usersRepository.GetUserlogin(Username, Password);
            return RedirectToPage("/UserPage", new { userid = User.UserId });
        }

        public IActionResult OnPostCreate()
        {
            return RedirectToPage("/CreateUser");
        }

    }
}
