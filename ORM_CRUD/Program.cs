using ORM_CRUD.Models;
using ORM_CRUD.Services;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        CategoryService categoryService = new CategoryService();
        ProductService productService = new ProductService();

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create Category");
            Console.WriteLine("2. Update Category");
            Console.WriteLine("3. Delete Category");
            Console.WriteLine("4. View Categories");
            Console.WriteLine("5. Create Product");
            Console.WriteLine("6. Update Product");
            Console.WriteLine("7. Delete Product");
            Console.WriteLine("8. View Products");
            Console.WriteLine("0. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreateCategory(categoryService);
                    break;
                case "2":
                    await UpdateCategory(categoryService);
                    break;
                case "3":
                    await DeleteCategory(categoryService);
                    break;
                case "4":
                    await ViewCategories(categoryService);
                    break;
                case "5":
                    await CreateProduct(productService);
                    break;
                case "6":
                    await UpdateProduct(productService);
                    break;
                case "7":
                    await DeleteProduct(productService);
                    break;
                case "8":
                    await ViewProducts(productService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateCategory(CategoryService categoryService)
    {
        Console.Write("Enter category name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Category name cannot be empty.");
            return;
        }

        var category = new Category { Name = name };
        await categoryService.CreateAsync(category);
        Console.WriteLine("Category created successfully.");
    }

    private static async Task UpdateCategory(CategoryService categoryService)
    {
        Console.Write("Enter category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        Console.Write("Enter new category name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Category name cannot be empty.");
            return;
        }

        var category = new Category { Id = id, Name = name };
        await categoryService.UpdateAsync(category);
        Console.WriteLine("Category updated successfully.");
    }

    private static async Task DeleteCategory(CategoryService categoryService)
    {
        Console.Write("Enter category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await categoryService.DeleteAsync(id);
        Console.WriteLine("Category deleted successfully.");
    }

    private static async Task ViewCategories(CategoryService categoryService)
    {
        var categories = await categoryService.GetCategoriesAsync();
        if (categories.Count == 0)
        {
            Console.WriteLine("No categories found.");
        }
        else
        {
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
        }
    }

    private static async Task CreateProduct(ProductService productService)
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Product name cannot be empty.");
            return;
        }

        Console.Write("Enter category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        Console.Write("Enter product price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price format.");
            return;
        }

        var product = new Product { Name = name, CategoryId = categoryId, Price = price };
        await productService.CreateAsync(product);
        Console.WriteLine("Product created successfully.");
    }

    private static async Task UpdateProduct(ProductService productService)
    {
        Console.Write("Enter product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        Console.Write("Enter new product name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Product name cannot be empty.");
            return;
        }

        Console.Write("Enter new category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        Console.Write("Enter new product price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price format.");
            return;
        }

        var product = new Product { Id = id, Name = name, CategoryId = categoryId, Price = price };
        await productService.UpdateAsync(product);
        Console.WriteLine("Product updated successfully.");
    }

    private static async Task DeleteProduct(ProductService productService)
    {
        Console.Write("Enter product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await productService.DeleteAsync(id);
        Console.WriteLine("Product deleted successfully.");
    }

    private static async Task ViewProducts(ProductService productService)
    {
        var products = await productService.GetCategoriesAsync();
        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }
}
