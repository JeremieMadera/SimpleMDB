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

        // 2. Repositorios (Capa de Datos - Acceso a las listas de 'db')
        var movieRepo = new MemoryMovieRepository(db);
        var actorRepo = new MemoryActorRepository(db);
        var userRepo = new MemoryUserRepository(db);
        var amRepo = new MemoryActorMovieRepository(db);

        // 3. Servicios (Capa de Negocio - Lógica de la aplicación)
        var movieServ = new DefaultMovieService(movieRepo);
        var actorServ = new MemoryActorService(actorRepo);
        var userServ = new MemoryUserService(userRepo);
        var amServ = new MemoryActorMovieService(amRepo);

        // 4. Controladores (Capa de API - Manejan las peticiones HTTP)
        var movieCtrl = new MoviesController(movieServ);
        var actorCtrl = new ActorsController(actorServ);
        var userCtrl = new UsersController(userServ);
        var amCtrl = new ActorsMoviesController(amServ);

        // 5. Routers Específicos (Definen las rutas de cada recurso)
        var movieRouter = new MoviesRouter(movieCtrl);
        var actorRouter = new ActorsRouter(actorCtrl);
        var userRouter = new UsersRouter(userCtrl);
        var amRouter = new ActorsMoviesRouter(amCtrl);

        // 6. Router Principal
        var apiRouter = new HttpRouter();

        // Middlewares Globales (Configuración estándar del servidor)
        router.Use(HttpUtils.StructuredLogging);
        router.Use(HttpUtils.CentralizedErrorHandling);
        router.Use(HttpUtils.AddResponseCorsHeaders);
        router.Use(HttpUtils.DefaultResponse);
        router.Use(HttpUtils.ParseRequestUrl);
        router.Use(HttpUtils.ParseRequestQueryString);
        router.UseParametrizedRouteMatching();

        // 7. REGISTRO DE RUTAS (VERSIONAMIENTO)
        // Registramos el prefijo /api/v1
        router.UseRouter("/api/v1", apiRouter);

        // Registramos cada recurso dentro de /api/v1
        apiRouter.UseRouter("/movies", movieRouter);         // -> /api/v1/movies
        apiRouter.UseRouter("/actors", actorRouter);         // -> /api/v1/actors
        apiRouter.UseRouter("/users", userRouter);           // -> /api/v1/users
        apiRouter.UseRouter("/actors-movies", amRouter);    // -> /api/v1/actors-movies
    }
}