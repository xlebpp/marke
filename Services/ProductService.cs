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
        Task<bool> IsThereAny();
    }
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) {
            _context = context;
        }

        public async Task<bool> IsThereAny()
        {
            if (_context.Products.Any()) { return true; }
            else { return false; }
        }
        public async Task<IEnumerable<ShowProducts>> GetProducts()
        {
            var products = await _context.Products
                .Include(m => m.Master)
                .Include(i => i.Images)
                .Select(p => new ShowProducts
                {
                    Name = p.Name,
                    IsService = p.IsService,
                    Price = p.Price,
                    MasterId = p.MasterId,
                    MasterName = p.Master.UserName,
                    Images = p.Images.Select(i => i.Url).ToList()
                }).ToListAsync();
            return products;



        }
    }
}