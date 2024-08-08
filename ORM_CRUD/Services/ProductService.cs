using ORM_CRUD.Context;
using ORM_CRUD.Exceptions;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_CRUD.Models
{
    public class ProductService
    {
        private readonly AppDbContext _appDbContext;
        public ProductService()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {
                throw new NotFoundException("Not found");
            }
            return product;
        }
        public async Task<List<Product>> GetCategoriesAsync()
        {
            var products = await _appDbContext.Products.AsNoTracking().ToListAsync();
            return products;
        }
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task CreateAsync(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Product cannot be null");
                return;
            }
            bool isExists = await _appDbContext.Products.AnyAsync(x => x.Name.ToLower() == product.Name.ToLower());
            if (isExists == true)
            {
                Console.WriteLine("Already exists");
                return;
            }

            var isExistAuthor = await _appDbContext.Categories.AnyAsync(x => x.Id == product.CategoryId);

            if (!isExistAuthor)
            {
                Console.WriteLine("Category tapilmadi");
                return;
            }

            if (product.Price < 0)
            {
                Console.WriteLine("Price 0 dan kicik ola bilmez");
                return;
            }
            product.Id = 0;
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product) 
        { 
            var existingProduct = await GetByIdAsync(product.Id);
            var isExists = await _appDbContext.Products.AnyAsync(_ => _.Name.ToLower() == product.Name.ToLower() && _.Id!=product.Id);
            if (isExists)
            {
                Console.WriteLine("Bu product artiq movcuddur");
                return;
            }
            var isExistAuthor = await _appDbContext.Categories.AnyAsync(x => x.Id == product.CategoryId);

            if (!isExistAuthor)
            {
                Console.WriteLine("Category tapilmadi");
                return;
            }

            if (product.Price < 0)
            {
                Console.WriteLine("Price 0 dan kicik ola bilmez");
                return;
            }
            product.Id = 0;
            existingProduct.Name = product.Name;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            _appDbContext.SaveChanges();
        }
    }
}
