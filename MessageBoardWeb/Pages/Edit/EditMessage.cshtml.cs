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
    public class EditMessageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<EditMessageModel> _logger;
        public EditMessageModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<EditMessageModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }
        [BindProperty]
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

            GetMessages = _messagesRepository.GetMessage(id);
            if (Title == null && Content == null)
            {
                _messagesRepository.UpdateMessage(GetMessages.MessageId, GetMessages.Title, GetMessages.Content, GetMessages.CategoryId);
                return RedirectToPage("../Details/DetaileMessages", new { id = GetMessages.MessageId });
            }
                
            if (Title == null)
            {
                _messagesRepository.UpdateMessage(GetMessages.MessageId, GetMessages.Title, Content, GetMessages.CategoryId);
                return RedirectToPage("../Details/DetaileMessages", new { id = GetMessages.MessageId });
            }
            if (Content == null)
            {
                _messagesRepository.UpdateMessage(GetMessages.MessageId, Title, GetMessages.Content, GetMessages.CategoryId);
                return RedirectToPage("../Details/DetaileMessages", new { id = GetMessages.MessageId });
            }
            else
                _messagesRepository.UpdateMessage(GetMessages.MessageId, Title, Content, GetMessages.CategoryId);

            return RedirectToPage("../Details/DetaileMessages", new { id = GetMessages.MessageId });

        }



    }
}
