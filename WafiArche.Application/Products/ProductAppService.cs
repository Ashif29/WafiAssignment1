using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiArche.Domain.Products;
using WafiArche.EntityFrameworkCore.Data;

namespace WafiArche.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly AppDbContext _context;

        public ProductAppService(AppDbContext context)
        {
            _context = context;
        }
        public Product CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public bool UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);
            return _context.SaveChanges() > 0;
        }
    }
}
