﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController: BaseController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productsBrandsRepo;
        private readonly IGenericRepository<ProductType> _productsTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController( IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productsBrandsRepo, 
            IGenericRepository<ProductType> productsTypeRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productsBrandsRepo = productsBrandsRepo;
            _productsTypeRepo = productsTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<List<ProductBrand>>>> GetProductBrands()
        {
            return Ok(await _productsBrandsRepo.ListAllAsync());
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<List<ProductType>>>> GetProductTypes()
        {
            return Ok(await _productsBrandsRepo.ListAllAsync());
        }
    }
}