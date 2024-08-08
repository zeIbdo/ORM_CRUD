using ORM_CRUD.Context;
using ORM_CRUD.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_CRUD.Exceptions;

namespace ORM_CRUD.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _appDbContext;
        public CategoryService()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task CreateAsync(Category category)
        {
            if (category == null)
            {
                Console.WriteLine("Category cannot be null");
                return ;
            }
                bool isExists = await _appDbContext.Categories.AnyAsync(x => x.Name.ToLower() == category.Name.ToLower());
            if (isExists == true) {
                Console.WriteLine("Already exists");
                return;
            }
            category.Id = 0;
            await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Category> GetByIdAsync(int id) 
        { 
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new NotFoundException("Not found");
            }
                return category;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _appDbContext.Categories.AsNoTracking().ToListAsync();
            return categories;
        }
        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await GetByIdAsync(category.Id);
            if (category == null)
            {
                Console.WriteLine("Category cannot be null");
                return;
            }
            bool isExists = await _appDbContext.Categories.AnyAsync(x => x.Name.ToLower() == category.Name.ToLower()&&x.Id!=category.Id);
            if (isExists == true)
            {
                Console.WriteLine("Already exists");
                return;
            }
            existingCategory.Name = category.Name;
            _appDbContext.SaveChanges();
        }
    }
}
