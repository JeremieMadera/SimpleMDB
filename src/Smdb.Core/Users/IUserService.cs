namespace Smdb.Core.Users;

using Shared.Http;

public interface IUserService
{
    // Listar usuarios con paginación
    Task<Result<PagedResult<User>>> ReadUsers(int page, int size);

    // Crear un usuario nuevo
    Task<Result<User>> CreateUser(User user);

    // Buscar un usuario por su ID
    Task<Result<User>> ReadUser(int id);

    // Actualizar datos de un usuario existente
    Task<Result<User>> UpdateUser(int id, User user);

    // Eliminar un usuario del sistema
    Task<Result<User>> DeleteUser(int id);
}