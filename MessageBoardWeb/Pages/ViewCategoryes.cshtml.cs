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
    public class ViewCategoryesModel : PageModel
    {
        #region Feltes and ctor
        private readonly ICategoryRepository _categoryRepos;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<ViewCategoryesModel> _logger;

        public ViewCategoryesModel(ICategoryRepository categoryRepos, IMessagesRepository messagesRepository, ILogger<ViewCategoryesModel> logger)
        {
            _messagesRepository = messagesRepository;
            _categoryRepos = categoryRepos;
            _logger = logger;
        }
        #endregion

        public SelectList Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedCategoryId { get; set; }
        public List<Messages> MessagesList { get; set; }
        public List<Category> Categorys { get; set; }



        public void OnGet()
        {
            MessagesList = _messagesRepository.ReadMessagesByCategory(SelectedCategoryId);
        }
    }
}
