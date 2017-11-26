using Catalogue.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalogue.Core.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Threading;

namespace Catalogue
{
    class ProductRepository : IProductRepository
    {
        private readonly IReliableStateManager _reliableStateManager;

        public ProductRepository(IReliableStateManager reliableStateManager)
        {
            _reliableStateManager = reliableStateManager;
        }

        public async Task AddOrUpdateAsync(Product product)
        {
            var products = await _reliableStateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");
            using (var tx = _reliableStateManager.CreateTransaction())
            {
                await products.AddOrUpdateAsync(tx, product.Id, product, (id, value) => product);
                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _reliableStateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");
            var result = new List<Product>();
            using (var tx = _reliableStateManager.CreateTransaction())
            {
                var allProducts = await products.CreateEnumerableAsync(tx, EnumerationMode.Unordered);
                using (var enumerator = allProducts.GetAsyncEnumerator())
                {
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        var current = enumerator.Current;
                        result.Add(current.Value);
                    }
                }
            }
            return result;
        }
    }
}
