using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserToRole;

public class AssignUserToRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string UserRole { get; set; } = default!;
}
