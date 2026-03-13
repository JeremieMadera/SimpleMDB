namespace Smdb.Core.Actors;

using Shared.Http; // Necesario para PagedResult y Result

public interface IActorService
{
    // L: Listar actores con paginación
    Task<Result<PagedResult<Actor>>> ReadActors(int page, int size);

    // C: Crear un nuevo actor
    Task<Result<Actor>> CreateActor(Actor actor);

    // R: Leer un actor por su ID
    Task<Result<Actor>> ReadActor(int id);

    // U: Actualizar los datos de un actor
    Task<Result<Actor>> UpdateActor(int id, Actor actor);

    // D: Borrar un actor
    Task<Result<Actor>> DeleteActor(int id);
}