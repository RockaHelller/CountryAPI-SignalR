using API_intro.Data;
using API_intro.DTOs.Country;
using API_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API_intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute][Required] int id)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null) return NotFound();
            return Ok(country);
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            List<Country> country = await _context.Countries.ToListAsync();
            return Ok(country);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = new()
            {
                Name = request.Name,
                Population = request.Population,
            };

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return Ok(country);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, Country updatedCountry)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            country.Name = updatedCountry.Name;
            country.Population = updatedCountry.Population;

            await _context.SaveChangesAsync();

            return Ok(country);
        }





        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);

            if (country == null) return NotFound();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        [Route("Search")]

        public async Task<ActionResult<IEnumerable<Country>>> SearchCountries(string country)
        {
            var countries = await _context.Countries.Where(c => c.Name.Contains(country)).ToListAsync();

            return Ok(countries);
        }
    }
}
