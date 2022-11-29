using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vakiti_API.Controllers
{
    [EnableCors("CORSpolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroEntityController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
            {
                new SuperHero{
                    Id = 1,
                    Name ="Sashman",
                    FirstName ="Sashidhar",
                    LastName="Vakiti",
                    Place="Sydney"
                },
                new SuperHero{
                    Id = 2,
                    Name ="RJ",
                    FirstName ="Preksha",
                    LastName="Vakiti",
                    Place="Sydney"
                }
            };
        public SuperHeroEntityController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {            
            return Ok(heros); //return full list of Heros
        }

        [HttpGet("{id}")] //We need to add the id as in the paramater for it to work
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = heros.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);// return only the hero who was found
        }

        [HttpPost]//Add
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            heros.Add(hero);
            return Ok(heros);
        }

        [HttpPut]//Update
        public async Task<ActionResult<List<SuperHero>>> UpdateHero([FromBody] SuperHero request)
        {
            var hero = heros.Find(h => h.Id == request.Id);
            if (hero == null)
                return BadRequest("Hero not found");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            return Ok(heros);//return full list of Heros
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = heros.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not found");

            heros.Remove(hero);//Delete the Hero
            return Ok(heros);//return full list of Heros
        }

    }
}
