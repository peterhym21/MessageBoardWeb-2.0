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
    public class DeleteMessageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<DeleteMessageModel> _logger;
        public DeleteMessageModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<DeleteMessageModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }
        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty]
        public string Category { get; set; }

        public Messages GetMessages { get; set; }

        public Category GetCategory { get; set; }

        public void OnGet(int id)
        {
            GetMessages = _messagesRepository.GetMessage(id);
            GetCategory = _categoryRepos.ReadOneCategories(GetMessages.CategoryId);
        }
        public IActionResult OnPost(int id)
        {
            _messagesRepository.DeleteMessage(id);
            return RedirectToPage("../Index");
        }
    }
}
