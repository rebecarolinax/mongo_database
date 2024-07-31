using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// armazena a config da aplicação
        /// </summary>
        private readonly IConfiguration _configuration;


        /// <summary>
        /// armazena referência ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// recebe a config da aplicação com parâmetro
        /// </summary>
        /// <param name="configuration">obj configuration</param>
        public MongoDbService(IConfiguration configuration)
        {
            //atribui a config recebida em _configuration
            _configuration = configuration;


            //obtêm a string de conexão através do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");

            //cria um obj MongoUrl que recebe como pârametro a string de conexão
            var mongoUrl = MongoUrl.Create(connectionString);

            //cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            //obtêm o nome ao bd com o nom eespecificado na string de conexão
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase GetDatabase => _database;
    }
}
