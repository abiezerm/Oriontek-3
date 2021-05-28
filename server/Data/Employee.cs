using System.ComponentModel.DataAnnotations;

namespace server.Data
{
  public class Employee
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string[] Address { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

  }
}