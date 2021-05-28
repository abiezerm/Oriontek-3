using System.Linq;
using server.Data;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using server.Inputs;

namespace server.GraphQl
{
    public class Query
    {
      public IQueryable<Company> GetCompanies([Service] AppDbContext context) =>
        context.Companies.Include(c => c.Employees);
      public Company GetCompany([Service] AppDbContext context, int id) =>
        context.Companies.Where(c => c.Id == id).Include(c => c.Employees).FirstOrDefault();
      
      public IQueryable<Employee> GetEmployees([Service] AppDbContext context) => 
        context.Employees;
      public IQueryable<Employee> GetEmployee([Service] AppDbContext context, int id) => 
        context.Employees.Where(e => e.Id == id);
      
      
    }
}