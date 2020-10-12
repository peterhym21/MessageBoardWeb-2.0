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
    public class CategorysModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<CategorysModel> _logger;

        public int SelectedCategoryId { get; set; }
        public List<Category> Categorys { get; set; }


        public CategorysModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<CategorysModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }

        public void OnGet()
        {
            Categorys = _categoryRepos.ReadCategories();
        }
    }
}
