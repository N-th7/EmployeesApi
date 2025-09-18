using Microsoft.AspNetCore.Mvc;
using AvancApi.Data;
using AvancApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AvancApi.Controllers
{

    [ApiController]
    [Route("api/v{version}/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public CompaniesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/companies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            return Ok(companies);
        }

        // GET: api/company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        // POST: api/companies
        [HttpPost]
        public async Task<IActionResult> Create(Company company, [FromRoute] string version)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = company.Id, version = version },
                company
            );
        }

        // PUT: api/companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Company company)
        {
            if (id != company.Id)
                return BadRequest();

            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}