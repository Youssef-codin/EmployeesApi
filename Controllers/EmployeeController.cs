using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeApi.Models.Entity;

namespace EmployeeApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class EmployeeController : ControllerBase
{

    private readonly McvContext c;

    public EmployeeController(McvContext dbContext)
    {
        this.c = dbContext;
    }

    [HttpGet]
    public IActionResult getAllEmps()
    {
        var emps = c.Employees.ToList();
        return Ok(emps);
    }

    [HttpPut("add1k")]
    public IActionResult add1KToEachEmp()
    {
        var emps = c.Employees.ToList();

        if (emps.Count() > 0)
        {
            foreach (Employee emp in emps)
            {
                emp.Salary += 1000;
            }
            c.SaveChanges();
            return Ok(emps);
        }

        return NotFound("No employees exist to do action");
    }

    [HttpPut("add25")]
    public IActionResult add25Percent()
    {
        var emps = c.Employees.ToList();

        if (emps.Count() > 0)
        {
            foreach (var emp in c.Employees)
            {
                emp.Salary *= 1.25m;

            }

            c.SaveChanges();
            return Ok(emps);
        }

        return NotFound();
    }

    [HttpPut("addPerCategory")]
    public IActionResult addPerCategory()
    {
        var emps = c.Employees;

        if (emps.Count() > 0)
        {
            List<Employee> empCategory1 = (from emp in emps
                                           where emp.Salary <= 50000
                                           select emp).ToList();

            List<Employee> empCategory2 = (from emp in emps
                                           where emp.Salary > 50000 && emp.Salary <= 75000
                                           select emp).ToList();

            List<Employee> empCategory3 = (from emp in emps
                                           where emp.Salary > 75000
                                           select emp).ToList();

            foreach (Employee emp in empCategory1)
            {
                emp.Salary *= 1.4m;
            }

            foreach (Employee emp in empCategory2)
            {
                emp.Salary *= 1.3m;
            }

            foreach (Employee emp in empCategory3)
            {
                emp.Salary *= 1.2m;
            }

            c.SaveChanges();
            return Ok(emps);
        }

        return NotFound();
    }

    [HttpGet("secondHighestSalary")]
    public IActionResult secondHighestSalary()
    {
        var emps = c.Employees;

        Employee? empSecondHighestSalary = (from emp in emps
                                            orderby emp.Salary descending
                                            select emp)
                                            .Skip(1)
                                            .FirstOrDefault();

        if (empSecondHighestSalary == null)
        {
            return NotFound();
        }

        return Ok(empSecondHighestSalary);
    }


    [HttpPut("Add25percetByDepartment")]
    public IActionResult Add25percetByDepartment(string deptToAddTo)
    {
        var emps = c.Employees;
        Department? Dept = (from dept in c.Departments
                            where deptToAddTo.Equals(dept.Name)
                            select dept).FirstOrDefault();
        if (Dept == null)
        {
            return NotFound("Dept doesnt exist");
        }

        var empsSelected = (from emp in emps
                            where emp.Department.Equals(deptToAddTo)
                            select emp);
        foreach (var emp in empsSelected)
        {
            emp.Salary *= 1.25m;
        }

        c.SaveChanges();
        return Ok();
    }

    [HttpGet("ShowData")]
    public IActionResult ShowData()
    {
        var emps = c.Employees;

        var query =
            from e in c.Employees
            join d in
                (from emp in c.Employees
                 group emp by emp.Department into g
                 select new
                 {
                     Department = g.Key,
                     DeptTotal = g.Sum(x => x.Salary)
                 })
            on e.Department equals d.Department
            orderby d.DeptTotal descending, e.Salary descending
            select new
            {
                e.Department,
                e.Manager,
                e.Name,
                e.Salary,
                d.DeptTotal
            };

        return Ok(query);
    }
}
