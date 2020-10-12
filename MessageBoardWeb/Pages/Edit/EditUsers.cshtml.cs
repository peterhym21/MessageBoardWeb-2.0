using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages.Edit
{
    public class EditUsersModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<EditUsersModel> _logger;
        public EditUsersModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<EditUsersModel> logger)
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

        [BindProperty]
        public string Passwordtwo { get; set; }

        public Users User { get; set; }

        public void OnGet(int userid)
        {
            User = _usersRepository.GetUser(userid);

        }

        public IActionResult OnPost(int userid)
        {
            User = _usersRepository.GetUser(userid);
            if (Password == Password && Username != null && Password != null && Passwordtwo != null)
            {
                _usersRepository.UpdateUser(Username, Password, userid);
            }
            if (Password == Password && Password != null && Passwordtwo != null)
            {
                _usersRepository.UpdateUser(User.UserName, Password, userid);
            }
            if (Username != null )
            {
                _usersRepository.UpdateUser(Username, User.Password, userid);
            }
            if (Username == null && Password == null && Passwordtwo == null)
            {
                _usersRepository.UpdateUser(User.UserName, User.Password, userid);
            }


            return RedirectToPage("../UserPage");
        }

    }
}
