using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseWork.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    //public Company Company { get; set; } = null!;
    public string? Country { get; set; }
    public string? City { get; set; }
    public bool? Horse { get; set; } = false;
    public List<RoleRelation> Roles { get; set; } = new List<RoleRelation>();
}