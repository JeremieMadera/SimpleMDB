namespace Smdb.Api.Actors;

using Shared.Http;

public class ActorsRouter : HttpRouter
{
    public ActorsRouter(ActorsController actorsController)
    {
        UseParametrizedRouteMatching();

        // Lista de actores (GET)
        MapGet("/", actorsController.ReadActors);

        // Crear actor (POST) - Importante usar ReadRequestBodyAsText
        MapPost("/", HttpUtils.ReadRequestBodyAsText, actorsController.CreateActor);

        // Leer uno solo (GET con ID)
        MapGet("/:id", actorsController.ReadActor);

        // Actualizar (PUT)
        MapPut("/:id", HttpUtils.ReadRequestBodyAsText, actorsController.UpdateActor);

        // Borrar (DELETE)
        MapDelete("/:id", actorsController.DeleteActor);
    }
}