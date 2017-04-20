using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Data;
using ManyToMany.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            this._db = db;
        }


        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return (from m in _db.Movies
                    select new Movie
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Director=m.Director,
                        MovieActors =
                        (from ma in m.MovieActors select new MovieActor { Actor = ma.Actor, ActorId = ma.Actor.Id, MovieId = ma.Movie.Id }).ToList()
                    }).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            else
            {
                var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
                return Ok(movie);
            }
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Actor actor)
        {
            _db.Actors.Add(actor);
            _db.SaveChanges();

            _db.MovieActors.Add(new MovieActor
            {
                MovieId = id,
                ActorId = actor.Id
            });
            _db.SaveChanges();

            return Ok();
        }
    }
}
