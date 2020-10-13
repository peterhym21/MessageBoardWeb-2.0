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
    public class DeleteCategorysModel : PageModel
    {
        #region Feltes and ctor
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<DeleteCategorysModel> _logger;

        public DeleteCategorysModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<DeleteCategorysModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }
        #endregion

        public List<Messages> MessagesList { get; set; }
        public Category GetCategory { get; set; }


        public void OnGet(int id)
        {
            GetCategory = _categoryRepos.ReadOneCategories(id);
        }


        public IActionResult OnPost(int id)
        {
            MessagesList = _messagesRepository.ReadMessagesByCategory(id);

            foreach (Messages message in MessagesList)
            {
                message.CategoryId = 1;
                _messagesRepository.UpdateMessage(message.MessageId, message.Title, message.Content, message.CategoryId);
            }

            _categoryRepos.DeleteCategory(id);
            return RedirectToPage("../Categorys");
        }
    }
}
