using Microsoft.Extensions.DependencyInjection;


using Api.Domain.Interfaces;
using Api.Data.Implementations;
using Api.CrossCutting.Repository;
using Api.Domain.Interfaces.Repositories;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {

        //private static string Host = "localhost";
        //private static string User = "Junior";
        //private static string DBname = "EventDB";
        //private static string Password = "123456";
        //private static string Port = "5432";

        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();


            //serviceCollection.AddDbContext<MyContext>(
            //     options => options.UseNpgsql($"Server={Host}; Port={Port}; Database={DBname}; Uid={User}; Pwd={Password}")
            //     );
        }
    }
}
