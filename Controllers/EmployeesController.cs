using Microsoft.AspNetCore.Mvc;
using AvancApi.Data;
using AvancApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AvancApi.Controllers
{

    [ApiController]
    [Route("api/v{version}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, [FromRoute] string version)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = employee.Id, version = version },
                employee
            );
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // // GET: api/employees?name=<name>
        // [HttpGet]
        // public async Task<IActionResult> GetAll([FromQuery] string? name)
        // {
        //     var query = _context.Employees.AsQueryable();

        //     if (!string.IsNullOrEmpty(name))
        //     {
        //         // Filtra por nombre que contenga la cadena (case-insensitive)
        //         query = query.Where(e => EF.Functions.ILike(e.Name, $"%{name}%"));
        //     }

        //     var employees = await query.ToListAsync();
        //     return Ok(employees);
        // }

    }
}
