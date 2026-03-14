namespace Smdb.Api;

using Shared.Http;
using Smdb.Api.Movies;
using Smdb.Api.Actors;
using Smdb.Api.Users;
using Smdb.Api.ActorsMovies;
using Smdb.Core.Movies;
using Smdb.Core.Actors;
using Smdb.Core.Users;
using Smdb.Core.ActorsMovies;
using Smdb.Core.Db;

public class App : HttpServer
{
    public override void Init()
    {
        // 1. Base de Datos Única (Contiene las listas de todo)
        var db = new MemoryDatabase();


        var movieRepo = new MemoryMovieRepository(db);
        var actorRepo = new MemoryActorRepository(db);
        var userRepo = new MemoryUserRepository(db);
        var amRepo = new MemoryActorMovieRepository(db);


        var movieServ = new DefaultMovieService(movieRepo);
        var actorServ = new MemoryActorService(actorRepo);
        var userServ = new MemoryUserService(userRepo);
        var amServ = new MemoryActorMovieService(amRepo);


        var movieCtrl = new MoviesController(movieServ);
        var actorCtrl = new ActorsController(actorServ);
        var userCtrl = new UsersController(userServ);
        var amCtrl = new ActorsMoviesController(amServ);


        var movieRouter = new MoviesRouter(movieCtrl);
        var actorRouter = new ActorsRouter(actorCtrl);
        var userRouter = new UsersRouter(userCtrl);
        var amRouter = new ActorsMoviesRouter(amCtrl);


        var apiRouter = new HttpRouter();

        // 2. MIDDLEWARES GLOBALES (Se ejecutan en orden, antes de llegar a las rutas)
        router.Use(HttpUtils.StructuredLogging);
        router.Use(HttpUtils.CentralizedErrorHandling);
        router.Use(HttpUtils.AddResponseCorsHeaders);
        router.Use(HttpUtils.DefaultResponse);
        router.Use(HttpUtils.ParseRequestUrl);
        router.Use(HttpUtils.ParseRequestQueryString);
        router.UseParametrizedRouteMatching();

        // 7. REGISTRO DE RUTAS (VERSIONAMIENTO)

        router.UseRouter("/api/v1", apiRouter);

        // Registramos cada recurso dentro de /api/v1
        apiRouter.UseRouter("/movies", movieRouter);         // -> /api/v1/movies
        apiRouter.UseRouter("/actors", actorRouter);         // -> /api/v1/actors
        apiRouter.UseRouter("/users", userRouter);           // -> /api/v1/users
        apiRouter.UseRouter("/actors-movies", amRouter);    // -> /api/v1/actors-movies
    }
}