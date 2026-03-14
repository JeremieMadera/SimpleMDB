namespace Smdb.Core.Users;

using Shared.Http;
using System.Net;

public class MemoryUserService : IUserService
{
    private IUserRepository userRepository;

    public MemoryUserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<PagedResult<User>>> ReadUsers(int page, int size)
    {
        if (page < 1)
            return new Result<PagedResult<User>>(new Exception("Page must be >= 1."), (int)HttpStatusCode.BadRequest);
        if (size < 1)
            return new Result<PagedResult<User>>(new Exception("Page size must be >= 1."), (int)HttpStatusCode.BadRequest);

        var pagedResult = await userRepository.ReadUsers(page, size);
        return pagedResult == null
            ? new Result<PagedResult<User>>(new Exception("Could not read users."), (int)HttpStatusCode.NotFound)
            : new Result<PagedResult<User>>(pagedResult, (int)HttpStatusCode.OK);
    }

    public async Task<Result<User>> CreateUser(User user)
    {
        var validation = ValidateUser(user);
        if (validation != null) return validation;

        var result = await userRepository.CreateUser(user);
        return result == null
            ? new Result<User>(new Exception("Could not create user."), (int)HttpStatusCode.InternalServerError)
            : new Result<User>(result, (int)HttpStatusCode.Created);
    }

    public async Task<Result<User>> ReadUser(int id)
    {
        var user = await userRepository.ReadUser(id);
        return user == null
            ? new Result<User>(new Exception($"User with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<User>(user, (int)HttpStatusCode.OK);
    }

    public async Task<Result<User>> UpdateUser(int id, User user)
    {
        var validation = ValidateUser(user);
        if (validation != null) return validation;

        var result = await userRepository.UpdateUser(id, user);
        return result == null
            ? new Result<User>(new Exception($"User with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<User>(result, (int)HttpStatusCode.OK);
    }

    public async Task<Result<User>> DeleteUser(int id)
    {
        var user = await userRepository.DeleteUser(id);
        return user == null
            ? new Result<User>(new Exception($"User with id {id} not found."), (int)HttpStatusCode.NotFound)
            : new Result<User>(user, (int)HttpStatusCode.OK);
    }

    private static Result<User>? ValidateUser(User? user)
    {
        if (user is null)
            return new Result<User>(new Exception("User payload is required."), (int)HttpStatusCode.BadRequest);
        if (string.IsNullOrWhiteSpace(user.Username))
            return new Result<User>(new Exception("Username is required."), (int)HttpStatusCode.BadRequest);
        if (string.IsNullOrWhiteSpace(user.Email))
            return new Result<User>(new Exception("Email is required."), (int)HttpStatusCode.BadRequest);
        return null;
    }
}
