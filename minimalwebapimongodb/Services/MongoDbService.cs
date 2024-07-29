using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        // armazena a configuração da aplicação
        private readonly IConfiguration _configuration;

        // armazena uma ref ao MongoDB
        private readonly IMongoDatabase _database;

        // contém a configuração necessária para acesso ao MongoDB
        public MongoDbService(IConfiguration configuration)
        {
            // atribui a configuração em _configuration
            _configuration = configuration;

            // acessa a string de conexão
            var connectionString = _configuration.GetConnectionString("DbConnection");

            // transforma a string obtida em MongoURL
            var mongoUrl = MongoUrl.Create(connectionString);

            // cria um client
            var mongoClient = new MongoClient(mongoUrl);

            // obtém a ref ao MongoDB
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        // propriedade para acessar o banco de dados => retorna os dados em _database
        public IMongoDatabase GetDatabase => _database;

    }
}
