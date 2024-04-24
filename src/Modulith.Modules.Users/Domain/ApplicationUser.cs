using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Modulith.Modules.Users.Contracts;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Users.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public ApplicationUser()
    {
    }

    public ApplicationUser(string email, string? fullName, string? phoneNumber, Address? address)
        : base(email)
    {
        Id = Guid.NewGuid();
        Email = Guard.Against.NullOrEmpty(email);
        UserName = Guard.Against.NullOrEmpty(email);
        FullName = Guard.Against.NullOrEmpty(fullName);
        PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber);
        Address = address;
    }

    [PersonalData] public string? FullName { get; set; }
    [PersonalData] public Address? Address { get; set; }

    public void Update(string? email, string? fullName, string? phone, Address? address)
    {
        Email = Guard.Against.NullOrEmpty(email);
        UserName = Guard.Against.NullOrEmpty(email);
        FullName = Guard.Against.NullOrEmpty(fullName);
        PhoneNumber = Guard.Against.NullOrEmpty(phone);
        Address = address;
    }
}