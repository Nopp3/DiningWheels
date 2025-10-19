using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Domain.Entities;
using DiningWheels.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IDiningWheelsDbContext _context;
    
    public CreateUserCommandHandler(IDiningWheelsDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        
        if (existing is not null)
            throw new InvalidOperationException($"User with email {request.Email} already exists.");
        
        string[] nameParts = request.FullName.Split(' ');
    
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = nameParts[0],
            LastName = nameParts.Length > 1 ? nameParts[1] : "",
            PasswordHash = request.Password, // Later it should be hashed
            Role = Role.Customer
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}