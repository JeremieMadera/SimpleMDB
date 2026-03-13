namespace Smdb.Api.ActorsMovies;

using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Shared.Http;
using Smdb.Core.ActorsMovies;

public class ActorsMoviesController
{
    private IActorMovieService amService;

    public ActorsMoviesController(IActorMovieService amService)
    {
        this.amService = amService;
    }

    // 1. GET /api/v1/actors-movies (Listado general)
    public async Task ReadActorsMovies(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 10;
        var result = await amService.ReadActorsMovies(page, size);
        await JsonUtils.SendPagedResultResponse<ActorMovie>(req, res, props, result, page, size);
        await next();
    }

    // 2. POST /api/v1/actors-movies (Vincular actor a película)
    public async Task CreateActorMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var text = (string)props["req.text"]!;
        var am = JsonSerializer.Deserialize<ActorMovie>(text, JsonSerializerOptions.Web);
        var result = await amService.CreateActorMovie(am!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // 3. GET /api/v1/actors-movies/:id (Ver una relación)
    public async Task ReadActorMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
        var result = await amService.ReadActorMovie(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // 4. PUT /api/v1/actors-movies/:id (Actualizar rol)
    public async Task UpdateActorMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
        var text = (string)props["req.text"]!;
        var am = JsonSerializer.Deserialize<ActorMovie>(text, JsonSerializerOptions.Web);
        var result = await amService.UpdateActorMovie(id, am!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // 5. DELETE /api/v1/actors-movies/:id (Eliminar relación)
    public async Task DeleteActorMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
        var result = await amService.DeleteActorMovie(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // 6. GET /api/v1/actors-movies/movie/:movieId (Filtrar por película)
    public async Task ReadByMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int movieId = int.TryParse(uParams["movieId"]!, out int i) ? i : -1;
        var result = await amService.ReadByMovie(movieId);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}