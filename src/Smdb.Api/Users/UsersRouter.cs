namespace Smdb.Api.Users;

using Shared.Http;

public class UsersRouter : HttpRouter
{
    public UsersRouter(UsersController usersController)
    {
        UseParametrizedRouteMatching();

        // Listar (GET)
        MapGet("/", usersController.ReadUsers);

        // Crear (POST)
        MapPost("/", HttpUtils.ReadRequestBodyAsText, usersController.CreateUser);

        // Obtener uno (GET por ID)
        MapGet("/:id", usersController.ReadUser);

        // Actualizar (PUT)
        MapPut("/:id", HttpUtils.ReadRequestBodyAsText, usersController.UpdateUser);

        // Borrar (DELETE)
        MapDelete("/:id", usersController.DeleteUser);
    }
}