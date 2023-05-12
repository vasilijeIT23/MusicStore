﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Customers.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase 
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Product>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllCustomers.Query());

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetCustomerById.Query { Id = id });

            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult Create(Product product)
        {
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(Product product)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(Product product)
        {
            return NoContent();
        }
    }
}
