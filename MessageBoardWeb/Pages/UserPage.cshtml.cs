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
    public class UserPageModel : PageModel
    {
        #region Feltes and ctor
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UserPageModel> _logger;
        public UserPageModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<UserPageModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        #endregion

        public int SelectedCategoryId { get; set; }
        public List<Messages> MessagesList { get; set; }
        public List<Category> Categorys { get; set; }
        public Users User { get; set; }

        public void OnGet(int userid)
        {
            User = _usersRepository.GetUser(userid);
            MessagesList = _messagesRepository.getMessageFromUser(userid);
            Categorys = _categoryRepos.ReadCategories();
        }
    }
}
