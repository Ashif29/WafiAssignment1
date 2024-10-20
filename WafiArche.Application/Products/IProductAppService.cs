using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiArche.Domain.Products;

namespace WafiArche.Application.Products
{
    public interface IProductAppService
    {
        Product CreateProduct(Product product);
        IEnumerable<Product> GetAll();
        Product GetProductById(int id);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int id);

    }
}
