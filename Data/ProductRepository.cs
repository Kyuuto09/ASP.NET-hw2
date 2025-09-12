using ASP_NET_hw2.Data;
using ASP_NET_hw2.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_hw2.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products.Include(p => p.Category);

        public Product? GetById(int id) =>
            _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetByCategory(int categoryId) =>
            _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToList();

        public IEnumerable<Product> GetByPrice(decimal min, decimal max) =>
            _context.Products.Where(p => p.Price >= min && p.Price <= max).Include(p => p.Category).ToList();

        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
