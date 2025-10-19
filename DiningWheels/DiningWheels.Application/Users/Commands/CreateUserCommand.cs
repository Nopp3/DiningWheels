namespace DiningWheels.Application.Users.Commands;

using MediatR;

public record CreateUserCommand(
    string Email,
    string FullName,
    string Password
) : IRequest<Guid>;
    