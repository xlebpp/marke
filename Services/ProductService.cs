using marketplaceE.appDbContext;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;
using marketplaceE.DTOs;
using Microsoft.EntityFrameworkCore;

namespace marketplaceE.Services
{

    public interface IProductService
    {
        Task<IEnumerable<ShowProducts>> GetProducts();
        Task<bool> IsThereAnyProduct();
        Task<bool> IsThereAnyMaster();
        Task<IEnumerable<ShowMasters>> SearchMasters(string search);
        Task<IEnumerable<ShowProducts>> SearchProducts(string search);
        Task<IEnumerable<ShowCategories>> SearchCategories(string search);
        Task<bool> IsThereAnyCategory();
        Task<ShowProduct> GetProduct(int id);
        Task<bool> DoesProductExsist(int id);
     }
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) {
            _context = context;
        }

        public async Task<bool> IsThereAnyProduct()
        {
            if (await _context.Products.AnyAsync()) { return true; }
            else { return false; }
        }
        public async Task<IEnumerable<ShowProducts>> GetProducts()
        {
            var products = await _context.Products
                .Include(m => m.Master)
                .Include(i => i.Images)
                .Include(c => c.Orders).ThenInclude(r => r.Review)
                .Include(p => p.Categories)
                .ToListAsync();

            var result = products.Select(p => new ShowProducts
            {
                ProductId = p.Id,
                Name = p.Name,
                IsService = p.IsService,
                Price = p.Price,
                MasterId = p.MasterId,
                MasterName = p.Master.UserName,
                Rating = p.Orders.Where(o => o.Review != null)
                                 .Select(o => o.Review.Rating)
                                 .DefaultIfEmpty(0)
                                 .Average(),
                Images = p.Images.Select(i => Convert.ToBase64String(i.Url)).ToList(),
                Categories = p.Categories.Select(c => c.Name).ToList()
            }).ToList();

            var random = new Random();
            var shuffled = result.OrderBy(o => random.Next()).ToList();
            return shuffled;

        }

        public async Task<ShowProduct> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(m => m.Master)
                .Include(i => i.Images)
                .Include(o => o.Orders).ThenInclude(r => r.Review)
                .FirstOrDefaultAsync(p => p.Id == id);

            var rating = product.Orders
                .Where(o => o.Review != null)
                .Select(o => o.Review.Rating)
                .DefaultIfEmpty(0)
                .Average();

            var showProduct = new ShowProduct
            {
                ProductId = product.Id,
                Name = product.Name,
                IsService = product.IsService,
                Description = product.Description,
                Price = product.Price,
                MasterId = product.MasterId,
                MasterName = product.Master.UserName,
                Rating = rating,
                Images = product.Images.Select(i => Convert.ToBase64String(i.Url)).ToList(),
                Categories = product.Categories.Select(i => i.Name).ToList()
            };

            return showProduct;

        }

        public async Task<bool> DoesProductExsist(int id)
        {
            var exsistance = await _context.Products.AnyAsync(i => i.Id == id);
            if (exsistance) { return true; }
            return false;
        }

        public async Task<bool> IsThereAnyMaster()
        {
            if (await _context.Users.AnyAsync(u => u.Role == RolesOfUsers.Master))
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<ShowMasters>> SearchMasters(string search)
        {
            var master = await _context.Users
                .Where(u => u.Role == RolesOfUsers.Master)
                .Where(u=>u.UserName.Contains(search))
                .Select(u => new ShowMasters
                {
                    MasterId = u.Id,
                    Name = u.UserName,
                    UserPhoto = u.UserPhoto!=null ? Convert.ToBase64String(u.UserPhoto) : null
                }).ToListAsync();

            var masters = master.OrderBy(m => m.Name.Equals(search, StringComparison.OrdinalIgnoreCase) ? 0 :
                                                m.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase) ? 1 : 2).ToList();
            return masters;
        }

        public async Task<IEnumerable<ShowProducts>> SearchProducts(string search)
        {
            var product = await _context.Products
                .Where(p => p.Name.Contains(search))
                .Include(m => m.Master)
                .Include(i => i.Images)
                .Select(p => new ShowProducts
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    IsService = p.IsService,
                    Price = p.Price,
                    MasterId = p.MasterId,
                    MasterName = p.Master.UserName,
                    Images = p.Images.Select(i => Convert.ToBase64String(i.Url)).ToList()
                }).ToListAsync();

            var products = product.OrderBy(p => p.Name.Equals(search, StringComparison.OrdinalIgnoreCase) ? 0 :
                                            p.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase) ? 1 : 2).ToList();
            return products;
        }

        public async Task<bool> IsThereAnyCategory()
        {
            if(await _context.Categories.AnyAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ShowCategories>> SearchCategories(string search)
        {
            var categories = await _context.Categories
                .Where(c => c.Name == search)
                .Select(c => new ShowCategories
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync();
            return categories;
        }


    }
}