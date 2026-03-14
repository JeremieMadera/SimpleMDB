namespace Smdb.Core.ActorsMovies;

using Shared.Http;
using System.Net;

public class MemoryActorMovieService : IActorMovieService
{
    private IActorMovieRepository repository;

    public MemoryActorMovieService(IActorMovieRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<ActorMovie>>> ReadActorsMovies(int page, int size)
    {
        if (page < 1)
            return new Result<PagedResult<ActorMovie>>(new Exception("Page must be >= 1."), (int)HttpStatusCode.BadRequest);
        if (size < 1)
            return new Result<PagedResult<ActorMovie>>(new Exception("Page size must be >= 1."), (int)HttpStatusCode.BadRequest);

        var pagedResult = await repository.ReadActorsMovies(page, size);
        return pagedResult == null
            ? new Result<PagedResult<ActorMovie>>(new Exception("Could not read actor-movies."), (int)HttpStatusCode.NotFound)
            : new Result<PagedResult<ActorMovie>>(pagedResult, (int)HttpStatusCode.OK);
    }

    public async Task<Result<ActorMovie>> CreateActorMovie(ActorMovie actorMovie)
    {
        var validation = Validate(actorMovie);
        if (validation != null) return validation;

        var result = await repository.CreateActorMovie(actorMovie);
        return result == null
            ? new Result<ActorMovie>(new Exception("Could not create actor-movie."), (int)HttpStatusCode.InternalServerError)
            : new Result<ActorMovie>(result, (int)HttpStatusCode.Created);
    }

    public async Task<Result<ActorMovie>> ReadActorMovie(int id)
    {
        var am = await repository.ReadActorMovie(id);
        return am == null
            ? new Result<ActorMovie>(new Exception($"ActorMovie with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<ActorMovie>(am, (int)HttpStatusCode.OK);
    }

    public async Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie actorMovie)
    {
        var validation = Validate(actorMovie);
        if (validation != null) return validation;

        var result = await repository.UpdateActorMovie(id, actorMovie);
        return result == null
            ? new Result<ActorMovie>(new Exception($"ActorMovie with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<ActorMovie>(result, (int)HttpStatusCode.OK);
    }

    public async Task<Result<ActorMovie>> DeleteActorMovie(int id)
    {
        var am = await repository.DeleteActorMovie(id);
        return am == null
            ? new Result<ActorMovie>(new Exception($"ActorMovie with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<ActorMovie>(am, (int)HttpStatusCode.OK);
    }

    public async Task<Result<List<ActorMovie>>> ReadByMovie(int movieId)
    {
        var list = await repository.ReadByMovie(movieId);
        return list == null
            ? new Result<List<ActorMovie>>(new Exception($"No actors found for movie {movieId}."), (int)HttpStatusCode.NotFound)
            : new Result<List<ActorMovie>>(list, (int)HttpStatusCode.OK);
    }

    private static Result<ActorMovie>? Validate(ActorMovie? am)
    {
        if (am is null)
            return new Result<ActorMovie>(new Exception("ActorMovie payload is required."), (int)HttpStatusCode.BadRequest);
        if (am.ActorId <= 0)
            return new Result<ActorMovie>(new Exception("ActorId must be > 0."), (int)HttpStatusCode.BadRequest);
        if (am.MovieId <= 0)
            return new Result<ActorMovie>(new Exception("MovieId must be > 0."), (int)HttpStatusCode.BadRequest);
        return null;
    }
}
