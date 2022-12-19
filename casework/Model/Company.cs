using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseWork.Model;

public class Company
{
    public int id { get; set; }
    [MaxLength(32)]
    public string name { get; set; }
    public User? userCreator { get; set; }

    public List<User> users { get; set; } = new List<User>();
}