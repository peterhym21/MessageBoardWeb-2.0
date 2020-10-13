using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MessageBoardWeb.Pages
{
    public class IndexModel : PageModel
    {
        #region Feltes and ctor
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, IUsersRepository usersRepository, ILogger<IndexModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _usersRepository = usersRepository;
            _logger = logger;
        }
        #endregion

        public SelectList Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<Messages> MessagesList { get; set; }
        public List<Category> Categorys { get; set; }
        public Users User { get; set; }




        public void OnGet(int userid)
        {
            User = _usersRepository.GetUser(userid);
            Categories = new SelectList(_categoryRepos.ReadCategories(), "CateogryId", "CategoryName");
            MessagesList = _messagesRepository.GetTopTen();
            Categorys = _categoryRepos.ReadCategories();
        }
    }
}
