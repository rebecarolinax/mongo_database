using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IMongoCollection<Product> _product;

        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            try
            {
                await _product.InsertOneAsync(product);
                return StatusCode(201, product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var filter = Builders<Product>.Filter.Eq(l => l.ID, id);
                var result = await _product.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                    return NotFound();

                return Ok("Produto deletado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Product updatedProduct)
        {

            try
            {
                updatedProduct.ID = id;

                var filter = Builders<Product>.Filter.Eq(l => l.ID, id);
                var update = Builders<Product>.Update.Set(l => l.Name, updatedProduct.Name).Set(l => l.Price, updatedProduct.Price).Set(l => l.AdditionalAttributes, updatedProduct.AdditionalAttributes);

                var result = await _product.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                    return NotFound();

                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.ID, id);
                var product = await _product.Find(filter).FirstOrDefaultAsync();

                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

