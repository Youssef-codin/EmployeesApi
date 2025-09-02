using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly McvContext c;

    public DepartmentController(McvContext dbContext)
    {
        this.c = dbContext;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult getAllDeps()
    {
        var deps = c.Departments.ToList();
        return Ok(deps);
    }

}
