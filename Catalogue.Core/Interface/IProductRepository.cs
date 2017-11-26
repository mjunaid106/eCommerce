using Catalogue.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.Core.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task AddOrUpdateAsync(Product product);
    }
}
