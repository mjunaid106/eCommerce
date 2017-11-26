using Catalogue.Core.Interface;
using Catalogue.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController()
        {
            _productService = ServiceProxy.Create<IProductService>(new Uri("fabric:/eCommerce/Catalogue"),
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService.GetAllProducts();
        }
    }
}
