using EventManager.Features.FilesMetadata;
using EventManager.Features.Registrations;
using EventManager.Features.Users;
using Marten;

namespace EventManager.Infrastructure
{
    public static class MartenConfiguration
    {
        public static void ConfigureMarten(this IServiceCollection services, string connectionString)
        {
            services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<User>().Identity(x =>  x.Id);
                options.Schema.For<Registration>().Identity(x => x.Id);
                options.Schema.For<FileMetadata>().Identity(x => x.Id);
                options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
            });
        }
    }
}
