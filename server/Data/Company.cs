using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server.Data
{
  public class Company
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Address { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

  }
}