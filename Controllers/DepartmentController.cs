using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers;

[Authorize(Roles = "Admin,Employee")]
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
    public IActionResult getAllDeps()
    {
        var deps = c.Departments.ToList();
        return Ok(deps);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("secret")]
    public IActionResult getSecretDeps()
    {
        var deps = c.Departments.ToList();
        return Ok(deps);
    }

    [HttpGet("HighestDeptSalary")]
    public IActionResult HighestDeptSalary()
    {
        string? topDept =
            (from emp in c.Employees
             group emp by emp.Department into g
             orderby g.Sum(emp => emp.Salary) descending
             select g.Key).FirstOrDefault();

        if (string.IsNullOrWhiteSpace(topDept))
        {
            return NotFound();
        }

        return Ok(topDept);
    }
}
