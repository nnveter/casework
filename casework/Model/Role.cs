using System.ComponentModel.DataAnnotations;

namespace CaseWork.Model;

public class Role
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; } = 0;
}