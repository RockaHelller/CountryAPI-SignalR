using API_intro.Data;
using API_intro.DTOs.Empoyee;
using API_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute][Required] int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = new()
            {
                FullName = request.FullName,
                Address = request.Address,
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery][Required] int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(m=>m.Id == id);

            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
