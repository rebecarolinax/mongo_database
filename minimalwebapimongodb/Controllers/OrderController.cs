using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using minimalwebapimongodb.Domains;
using MongoDB.Driver;

namespace minimalwebapimongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _order;

        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
        }


        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var Orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                return Ok(Orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Order order)
        {
            try
            {
                await _order.InsertOneAsync(order);
                return StatusCode(201, order);
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
                var filter = Builders<Order>.Filter.Eq(l => l.ID, id);
                var result = await _order.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                    return NotFound();

                return Ok("Pedido deletado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Order updatedOrder)
        {

            try
            {
                updatedOrder.ID = id;

                var filter = Builders<Order>.Filter.Eq(l => l.ID, id);
                var update = Builders<Order>.Update.Set(l => l.Status, updatedOrder.Status).Set(l => l.Date, updatedOrder.Date);

                var result = await _order.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                    return NotFound();

                return Ok("Pedido atualizado com sucesso!");
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
                var filter = Builders<Order>.Filter.Eq(p => p.ID, id);
                var Order = await _order.Find(filter).FirstOrDefaultAsync();

                if (Order == null)
                    return NotFound();

                return Ok(Order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

