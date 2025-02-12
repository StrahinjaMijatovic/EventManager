using EventManager.Features.FilesMetadata;
using EventManager.Features.Registrations;
using EventManager.Features.Users;
using Marten;
using Microsoft.AspNetCore.Identity;
using Weasel.Core;

namespace EventManager.Infrastructure
{
    public static class MartenConfiguration
    {
        public static void ConfigureMarten(this IServiceCollection services, string connectionString)
        {
            services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<User>().Identity(x => x.Id);
                options.Schema.For<Registration>().Identity(x => x.Id);
                options.Schema.For<FileMetadata>().Identity(x => x.Id);

                // Dodaj ASP.NET Identity entitete u Marten šemu
                options.Schema.For<IdentityUser>().Identity(x => x.Id);
                options.Schema.For<IdentityRole>().Identity(x => x.Id);
                options.Schema.For<IdentityUserRole<string>>().Identity(x => x.UserId);
                options.Schema.For<IdentityUserClaim<string>>().Identity(x => x.Id);
                options.Schema.For<IdentityUserLogin<string>>().Identity(x => x.UserId);
                options.Schema.For<IdentityRoleClaim<string>>().Identity(x => x.Id);
                options.Schema.For<IdentityUserToken<string>>().Identity(x => x.UserId);

                options.AutoCreateSchemaObjects = AutoCreate.All;
            });
        }
    }
}
