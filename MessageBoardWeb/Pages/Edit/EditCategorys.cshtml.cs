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
    public class EditCategorysModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<EditCategorysModel> _logger;
        public EditCategorysModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<EditCategorysModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }


        [BindProperty]
        public string Category { get; set; }


        public Category GetCategory { get; set; }

        public void OnGet(int id)
        {
            GetCategory = _categoryRepos.ReadOneCategories(id);
        }
        public IActionResult OnPost(int id)
        {
            GetCategory = _categoryRepos.ReadOneCategories(id);

            if (Category == null)
            {
                _categoryRepos.UpdateCategory(GetCategory.CategoryName, id);
                return RedirectToPage("../Categorys");
            }
            else
                _categoryRepos.UpdateCategory(Category, id);

            return RedirectToPage("../Categorys");

        }
    }
}
