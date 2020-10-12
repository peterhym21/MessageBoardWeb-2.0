using MessageBoardRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> ReadCategories();
        Category ReadOneCategories(int Id);
        int CreateCategory(string NewCategoryName);
        int UpdateCategory(string CategoryRename, int CategoryId);
        int DeleteCategory(int CategoryId);
    }
}
