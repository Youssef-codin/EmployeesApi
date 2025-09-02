using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeApi.Models.Entity;

namespace EmployeeApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly McvContext c;

    public EmployeeController(McvContext dbContext)
    {
        this.c = dbContext;
    }

    [AllowAnonymous]
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
}
