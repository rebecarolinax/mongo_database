using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalwebapimongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                await _user.InsertOneAsync(user);
                return StatusCode(201, user);
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
                var filter = Builders<User>.Filter.Eq(l => l.ID, id);
                var result = await _user.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                    return NotFound();

                return Ok("Usuário deletado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, User updatedUser)
        {

            try
            {
                updatedUser.ID = id;

                var filter = Builders<User>.Filter.Eq(l => l.ID, id);
                var update = Builders<User>.Update.Set(l => l.Name, updatedUser.Name).Set(l => l.Name, updatedUser.Name).Set(l => l.AdditionalAttributes,updatedUser.AdditionalAttributes).Set(l => l.Email, updatedUser.Email).Set(l => l.Password, updatedUser.Password);

                var result = await _user.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                    return NotFound();

                return Ok("Usuário atualizado com sucesso!");
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
                var filter = Builders<User>.Filter.Eq(p => p.ID, id);
                var user = await _user.Find(filter).FirstOrDefaultAsync();

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

