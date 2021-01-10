using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController: BaseController
    {
        private readonly IProductInterface _repo;

        public ProductsController(IProductInterface repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
        
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<List<ProductBrand>>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<List<ProductType>>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }
    }
}