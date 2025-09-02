using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models.Entity;

namespace EmployeeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly McvContext c;

    public DepartmentController(McvContext dbContext)
    {
        this.c = dbContext;
    }

    [HttpGet]
    public IActionResult getAllEmps()
    {
        var emps = c.Employees.ToList();
        return Ok(emps);
    }

    [HttpPut]
    public IActionResult add1KToEachEmp()
    {
        var emps = c.Employees.ToList();

        foreach (Employee emp in emps)
        {
            emp.Salary += 1000;
        }

        c.SaveChanges();
        return Ok();
    }
}
