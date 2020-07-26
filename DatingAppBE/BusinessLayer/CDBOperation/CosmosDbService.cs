using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayerAbstraction.CDBAbs;
using Microsoft.Azure.Cosmos;

namespace BusinessLayer.CDBOperation
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        #region Constructor

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        #endregion

        public void Add<T>(T entity) where T : class
        {
            _container.CreateItemAsync<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<ModelLayer.Test> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ModelLayer.Test>> GetUsers()
        {
            var query = this._container.GetItemQueryIterator<ModelLayer.Test>(new QueryDefinition("SELECT * FROM c"));
            List<ModelLayer.Test> results = new List<ModelLayer.Test>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results; 
        }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }

        


    }
}
