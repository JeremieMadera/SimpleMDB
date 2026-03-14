namespace Smdb.Core.Actors;

using Shared.Http;
using System.Net;

public class MemoryActorService : IActorService
{
    private IActorRepository actorRepository;

    public MemoryActorService(IActorRepository actorRepository)
    {
        this.actorRepository = actorRepository;
    }

    public async Task<Result<PagedResult<Actor>>> ReadActors(int page, int size)
    {
        if (page < 1)
            return new Result<PagedResult<Actor>>(new Exception("Page must be >= 1."), (int)HttpStatusCode.BadRequest);
        if (size < 1)
            return new Result<PagedResult<Actor>>(new Exception("Page size must be >= 1."), (int)HttpStatusCode.BadRequest);

        var pagedResult = await actorRepository.ReadActors(page, size);
        return pagedResult == null
            ? new Result<PagedResult<Actor>>(new Exception($"Could not read actors."), (int)HttpStatusCode.NotFound)
            : new Result<PagedResult<Actor>>(pagedResult, (int)HttpStatusCode.OK);
    }

    public async Task<Result<Actor>> CreateActor(Actor actor)
    {
        var validation = ValidateActor(actor);
        if (validation != null) return validation;

        var result = await actorRepository.CreateActor(actor);
        return result == null
            ? new Result<Actor>(new Exception("Could not create actor."), (int)HttpStatusCode.InternalServerError)
            : new Result<Actor>(result, (int)HttpStatusCode.Created);
    }

    public async Task<Result<Actor>> ReadActor(int id)
    {
        var actor = await actorRepository.ReadActor(id);
        return actor == null
            ? new Result<Actor>(new Exception($"Actor with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<Actor>(actor, (int)HttpStatusCode.OK);
    }

    public async Task<Result<Actor>> UpdateActor(int id, Actor actor)
    {
        var validation = ValidateActor(actor);
        if (validation != null) return validation;

        var result = await actorRepository.UpdateActor(id, actor);
        return result == null
            ? new Result<Actor>(new Exception($"Actor with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<Actor>(result, (int)HttpStatusCode.OK);
    }

    public async Task<Result<Actor>> DeleteActor(int id)
    {
        var actor = await actorRepository.DeleteActor(id);
        return actor == null
            ? new Result<Actor>(new Exception($"Actor with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<Actor>(actor, (int)HttpStatusCode.OK);
    }

    private static Result<Actor>? ValidateActor(Actor? actor)
    {
        if (actor is null)
            return new Result<Actor>(new Exception("Actor payload is required."), (int)HttpStatusCode.BadRequest);
        if (string.IsNullOrWhiteSpace(actor.Name))
            return new Result<Actor>(new Exception("Name is required."), (int)HttpStatusCode.BadRequest);
        return null;
    }
}
