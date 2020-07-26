using BusinessLayer.Auth;
using BusinessLayer.CDBOperation;
using BusinessLayer.UserOperation;
using BusinessLayerAbstraction.Auth;
using BusinessLayerAbstraction.CDBAbs;
using BusinessLayerAbstraction.UserAbs;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class BusinessExtension
    {
        
        public static IServiceCollection BusinessLibararyExtension(this IServiceCollection services)
        {            
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult());
            return services;
        }

        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync()
        {
            string databaseName = "DatingAppDB";
            string containerName = "UserDb";
            string cosmosEndpoint = "https://localhost:8081";
            string key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";           
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(cosmosEndpoint, key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
            CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/Username");

            return cosmosDbService;
        }
    }
}
