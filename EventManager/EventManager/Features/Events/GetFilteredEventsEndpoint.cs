using Carter;
using Marten;

namespace EventManager.Features.Events
{
    public class GetFilteredEventsEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/events", async ([AsParameters] EventFilterQuery query, IDocumentSession session) =>
            {
                var eventsQuery = session.Query<Event>();

                //TO DO


            });
        }
    }
}
