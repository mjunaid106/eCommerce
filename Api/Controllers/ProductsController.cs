using Catalogue.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Product 1 Description",
                Price = 10
            };

            return new List<Product> { product };
        }
    }
}
