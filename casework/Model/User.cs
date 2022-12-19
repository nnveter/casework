using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseWork.Model;

public class User
{
    public int id { get; set; }

    public string email { get; set; }
    public string password { get; set; }
    
    public string? firstName { get; set; }
    public string? lastName { get; set; }

    //public Company Company { get; set; } = null!;
    public string? country { get; set; }
    public string? city { get; set; }
    public bool? horse { get; set; } = false;
    public List<RoleRelation> roles { get; set; } = new List<RoleRelation>();
}