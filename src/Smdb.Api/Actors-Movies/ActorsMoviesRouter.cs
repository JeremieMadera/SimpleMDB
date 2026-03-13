namespace Smdb.Api.ActorsMovies;

using Shared.Http;

public class ActorsMoviesRouter : HttpRouter
{
    public ActorsMoviesRouter(ActorsMoviesController amController)
    {
        UseParametrizedRouteMatching();

        MapGet("/", amController.ReadActorsMovies);
        MapPost("/", HttpUtils.ReadRequestBodyAsText, amController.CreateActorMovie);
        MapGet("/:id", amController.ReadActorMovie);
        MapPut("/:id", HttpUtils.ReadRequestBodyAsText, amController.UpdateActorMovie);
        MapDelete("/:id", amController.DeleteActorMovie);

        // Ruta extra para el endpoint #21
        MapGet("/movie/:movieId", amController.ReadByMovie);
    }
}