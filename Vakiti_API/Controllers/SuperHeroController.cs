using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vakiti_API.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {        
        private readonly DataCon _dataCon;

        public SuperHeroController(DataCon dataCon)
        {
            _dataCon = dataCon;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {            
            return Ok(await _dataCon.SuperHeroes.ToListAsync()); //return full list of Heros from DB
        }

        [HttpGet("{id}")] //We need to add the id as in the paramater for it to work
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _dataCon.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found in DB");
            return Ok(hero);// return only the hero who was found
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            _dataCon.SuperHeroes.Add(hero);
            await _dataCon.SaveChangesAsync();
            return Ok(await _dataCon.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero([FromBody] SuperHero request)
        {
            var dbhero = await _dataCon.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("Hero not found in DB");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;
            await _dataCon.SaveChangesAsync();

            return Ok(await _dataCon.SuperHeroes.ToListAsync());//return full list of Heros from DB after updating
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var dbhero = await _dataCon.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not found in DB");

            _dataCon.SuperHeroes.Remove(dbhero);//Delete the Hero
            await _dataCon.SaveChangesAsync();
            return Ok(await _dataCon.SuperHeroes.ToListAsync());//return full list of Heros from DB after deletion
        }

    }
}
