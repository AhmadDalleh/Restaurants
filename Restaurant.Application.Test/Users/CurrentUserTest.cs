using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;

namespace Restaurant.Application.Test.Users;

public class CurrentUserTest
{
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
        
        //  Act
        var isInRole = currentUser.IsInRole(roleName);

        // Assert
        isInRole.Should().BeTrue();

    }


    [Fact()]
    public void IsInRole_WitNohMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //  Act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // Assert
        isInRole.Should().BeFalse();

    }


    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //  Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // Assert
        isInRole.Should().BeFalse();

    }
}
