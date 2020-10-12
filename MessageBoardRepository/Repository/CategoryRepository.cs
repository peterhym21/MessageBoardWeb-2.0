using MessageBoardRepository.Entities;
using MessageBoardRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardRepository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _conString;
        private List<Category> categories;

        public CategoryRepository(string conString)
        {
            _conString = conString;
        }

        public List<Category> ReadCategories()
        {
            categories = new List<Category>();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("ReadCategorys", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                categories.Add(new Category { CateogryId = (int)myReader["CategoryId"], CategoryName = (string)myReader["CategoryName"] });
            }

            con.Close();
            return categories;
        }


        public int CreateCategory(string NewCategoryName)
        {
            categories = new List<Category>();
            int CategoryId = 0;

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("CreateCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewCategoryName", NewCategoryName);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            categories = ReadCategories();

            foreach (Category category in categories)
            {
                if (category.CategoryName == NewCategoryName)
                {
                    CategoryId = category.CateogryId;
                }
            }
            return CategoryId;
        }


        public int UpdateCategory(string CategoryRename, int CategoryId)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("UpdateCategorys", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CategoryRename", CategoryRename);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);

            con.Open();
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            return affectedRows;
        }


        public int DeleteCategory(int CategoryId)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("UpdateCategorys", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);

            con.Open();
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            return affectedRows;
        }
    }
}
