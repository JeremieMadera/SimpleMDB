namespace Smdb.Core.ActorsMovies;

using Shared.Http;

public interface IActorMovieService
{
    // 1. Listar todas las relaciones actor-película (Paginado)
    Task<Result<PagedResult<ActorMovie>>> ReadActorsMovies(int page, int size);

    // 2. Vincular un actor a una película
    Task<Result<ActorMovie>> CreateActorMovie(ActorMovie actorMovie);

    // 3. Ver una relación específica por ID
    Task<Result<ActorMovie>> ReadActorMovie(int id);

    // 4. Actualizar el rol de un actor en una película
    Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie actorMovie);

    // 5. Eliminar un actor de una película
    Task<Result<ActorMovie>> DeleteActorMovie(int id);

    // 6. Listar actores por película específica (Endpoint extra para completar los 21)
    Task<Result<List<ActorMovie>>> ReadByMovie(int movieId);
}