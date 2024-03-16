using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;
using System.Reflection.Metadata.Ecma335;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
           this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
           
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null)
                return NotFound("Hero not found.");

            return Ok(hero);

        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
             _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok( await _context.SuperHeroes.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var heroi = await _context.SuperHeroes.FindAsync(hero.Id);
            if (heroi is null)
                return NotFound("Hero not found.");
            heroi.Name = hero.Name;
            heroi.FirstName = hero.FirstName;
            heroi.LastName = hero.LastName;
            heroi.Place = hero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var heroi = await _context.SuperHeroes.FindAsync(id);
            if (heroi is null)
                return NotFound("Hero not found.");

            _context.SuperHeroes.Remove(heroi);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

    }
}
