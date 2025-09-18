using Microsoft.EntityFrameworkCore;
using AvancApi.Models;

namespace AvancApi.Data
{
    public class CompanyRepository
    {
        private readonly EmployeeContext _context;

        public CompanyRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync() => await _context.Companies.ToListAsync();

        public async Task<Company?> GetByIdAsync(int id) => await _context.Companies.FindAsync(id);

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
