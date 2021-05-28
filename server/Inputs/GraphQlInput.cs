namespace server.Inputs
{
  public record UpdateCompanyInput(
      int Id,
      string Name,
      string Address
  );
  public record AddCompanyInput(
      string Name,
      string Address,
      EmployeeInput[] Employees
  );
  public record EmployeeInput(
      string Name,
      string[] Address
  );

  public record UpdateEmployeeInput(
      int Id,
      string Name,
      string[] Address
  );

  public record AddEmployeeInput(
      int CompanyId,
      string Name,
      string[] Address
  );
  public record AddEmployeesInput(
      int CompanyId,
      EmployeeInput[] Employees
  );
}