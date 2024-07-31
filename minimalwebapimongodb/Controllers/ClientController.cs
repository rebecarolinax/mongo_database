using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalwebapimongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _client;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
        }


        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);
                return StatusCode(201, client);
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
                var filter = Builders<Client>.Filter.Eq(l => l.ID, id);
                var result = await _client.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                    return NotFound();

                return Ok("Cliente deletado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Client updatedClient)
        {

            try
            {
                updatedClient.ID = id;

                var filter = Builders<Client>.Filter.Eq(l => l.ID, id);
                var update = Builders<Client>.Update.Set(l => l.CPF, updatedClient.CPF).Set(l => l.Phone, updatedClient.Phone).Set(l => l.AdditionalAttributes, updatedClient.AdditionalAttributes).Set(l => l.Address, updatedClient.Address);

                var result = await _client.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                    return NotFound();

                return Ok("Cliente atualizado com sucesso!");
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
                var filter = Builders<Client>.Filter.Eq(p => p.ID, id);
                var client = await _client.Find(filter).FirstOrDefaultAsync();

                if (client == null)
                    return NotFound();

                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
