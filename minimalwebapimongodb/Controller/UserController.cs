using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controller
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
        public async Task<ActionResult<List<User>>> get()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var user = await _user.Find(x => x.Id == id).FirstOrDefaultAsync();
                return Ok(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                await _user.DeleteOneAsync(filter);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(string id, User user)
        {
            try
            {

                await _user.ReplaceOneAsync(z => z.Id == id, user);

                return Ok("Usuário atualizado com sucesso");
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(User user)
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

        

    }
}
