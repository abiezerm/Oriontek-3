using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Inputs;

namespace server.GraphQl
{
  public class Mutation
  {
    public async Task<Company> AddCompanyAsync(
      [Service] AppDbContext context,
      AddCompanyInput input)
      {
        var company = new Company
        {
          Name = input.Name,
          Address = input.Address
        };
        foreach (var emp in input.Employees)
        {
            var employee = new Employee
            {
              Name = emp.Name,
              Address = emp.Address
            };
            company.Employees.Add(employee);
        }
        context.Companies.Add(company);
        await context.SaveChangesAsync();
        return company;
      }

      public async Task<Company> UpdateCompanyAsync(
        [Service] AppDbContext context, 
        UpdateCompanyInput input)
      {
        var company = context.Companies.Where(c => c.Id == input.Id).Include(c=>c.Employees).FirstOrDefault();
        company.Name = input.Name;
        company.Address = input.Address;
        
        await context.SaveChangesAsync();
        return company;
      }

      public async Task<Company> DeleteCompanyAsync(
        [Service] AppDbContext context, 
        int id)
      {
        var company = context.Companies.Where(c => c.Id == id).Include(c=>c.Employees).FirstOrDefault();
        var emp = context.Employees.Where(e => e.CompanyId == id);
        context.Employees.RemoveRange(emp);
        context.Companies.Remove(company);
        await context.SaveChangesAsync();
        return company;
      }

      public async Task<Employee> AddEmployeeAsync(
        [Service] AppDbContext context,
        AddEmployeeInput input)
      {
        var company = context.Companies.Where(c => c.Id == input.CompanyId).FirstOrDefault();
        var employee = new Employee
        {
          Name = input.Name,
          Address = input.Address
        };
        company.Employees.Add(employee);
        await context.SaveChangesAsync();
        return employee;
      }

      public async Task<ICollection<Employee>> AddEmployeesAsync(
        [Service] AppDbContext context,
        AddEmployeesInput input)
      {
        var company = context.Companies.Where(c => c.Id == input.CompanyId).FirstOrDefault();

        foreach (var emp in input.Employees)
        {
            var employee = new Employee
            {
              Name = emp.Name,
              Address = emp.Address
            };
            company.Employees.Add(employee);
        }
        await context.SaveChangesAsync();
        return company.Employees;
      }

      public async Task<Employee> UpdateEmployee(
        [Service] AppDbContext context,
        UpdateEmployeeInput input)
      {
        var employee = context.Employees.Where(e => e.Id == input.Id).FirstOrDefault();
        employee.Name = input.Name;
        employee.Address = input.Address;

        await context.SaveChangesAsync();
        return employee;
      }

      public async Task<Employee> DeleteEmployee(
        [Service] AppDbContext context,
        int id)
      {
        var employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return employee;
        
      }
  }
}