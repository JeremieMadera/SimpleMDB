namespace Smdb.Core.ActorsMovies;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryActorMovieRepository : IActorMovieRepository
{
    private MemoryDatabase db;
    private int nextId = 0;

    public MemoryActorMovieRepository(MemoryDatabase db)
    {
        this.db = db;
        nextId = db.ActorsMovies.Count;
    }

    public async Task<PagedResult<ActorMovie>?> ReadActorsMovies(int page, int size)
    {
        int totalCount = db.ActorsMovies.Count;
        int start = Math.Clamp((page - 1) * size, 0, totalCount);
        int length = Math.Clamp(size, 0, totalCount - start);
        var values = db.ActorsMovies.Slice(start, length);
        return await Task.FromResult(new PagedResult<ActorMovie>(totalCount, values));
    }

    public async Task<ActorMovie?> CreateActorMovie(ActorMovie newActorMovie)
    {
        newActorMovie.Id = ++nextId;
        db.ActorsMovies.Add(newActorMovie);
        return await Task.FromResult(newActorMovie);
    }

    public async Task<ActorMovie?> ReadActorMovie(int id)
    {
        return await Task.FromResult(db.ActorsMovies.FirstOrDefault(am => am.Id == id));
    }

    public async Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData)
    {
        var am = db.ActorsMovies.FirstOrDefault(am => am.Id == id);
        if (am != null)
        {
            am.ActorId = newData.ActorId;
            am.MovieId = newData.MovieId;
            am.Role = newData.Role;
        }
        return await Task.FromResult(am);
    }

    public async Task<ActorMovie?> DeleteActorMovie(int id)
    {
        var am = db.ActorsMovies.FirstOrDefault(am => am.Id == id);
        if (am != null) { db.ActorsMovies.Remove(am); }
        return await Task.FromResult(am);
    }

    public async Task<List<ActorMovie>?> ReadByMovie(int movieId)
    {
        var result = db.ActorsMovies.Where(am => am.MovieId == movieId).ToList();
        return await Task.FromResult(result);
    }
}
