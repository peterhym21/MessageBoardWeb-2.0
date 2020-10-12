using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<CreateUserModel> _logger;
        public CreateUserModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<CreateUserModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [BindProperty, Required]
        public string Username { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty, Required]
        public string Passwordtwo { get; set; }

        public int Userid { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {

            }
            else
            {
                if (Password == Passwordtwo)
                {
                    Userid = _usersRepository.CreateUseres(Username, Password);
                }
                return RedirectToPage("/UserPage", new { userid = Userid });
            }

            return Page();
        }
    }
}
