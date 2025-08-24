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
            var masters = await _context.Users
                .Where(u => u.Role == RolesOfUsers.Master)
                .Where(u=>u.UserName==search)
                .Select(u => new ShowMasters
                {
                    MasterId = u.Id,
                    Name = u.UserName,
                    UserPhoto = u.UserPhoto
                }).ToListAsync();
            return masters;
        }

        public async Task<IEnumerable<ShowProducts>> SearchProducts(string search)
        {
            var products = await _context.Products
                .Where(p => p.Name == search)
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

        public async Task<bool> IsThereAnyCategory()
        {
            if(await _context.)
        }

        public async Task<IEnumerable<ShowCategories>> SearchCategories(string search)
        {
            var categories = await _context.C
        }
    }
}