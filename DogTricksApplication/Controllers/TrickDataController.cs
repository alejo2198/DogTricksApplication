using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DogTricksApplication.Models;

namespace DogTricksApplication.Controllers
{
    public class TrickDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TrickData/ListTricks
        [HttpGet]
        public IEnumerable<TrickDto> ListTricks()
        {
            List<Trick> Tricks = db.Tricks.ToList();
            List<TrickDto> TrickDtos = new List<TrickDto>();

            Tricks.ForEach(trick => TrickDtos.Add(new TrickDto()
            {
                TrickId = trick.TrickId,
                TrickName = trick.TrickName,
                TrickDescription = trick.TrickDescription,
                TrickDifficulty = trick.TrickDifficulty,
                TrickVideoLink = trick.TrickVideoLink
            }));
            return TrickDtos;
        }

    

        // GET: api/TrickData/FindTrick/5
        [ResponseType(typeof(Trick))]
        [HttpGet]
        public IHttpActionResult FindTrick(int id)
        {
            Trick trick = db.Tricks.Find(id);
            if (trick == null)
            {
                return NotFound();
            }
            TrickDto TrickDto = new TrickDto()
            {
                TrickId = trick.TrickId,
                TrickName = trick.TrickName,
                TrickDescription = trick.TrickDescription,
                TrickDifficulty = trick.TrickDifficulty,
                TrickVideoLink = trick.TrickVideoLink
            };

            return Ok(TrickDto);
        }
  

        // GET: api/TrickData/ ListTricksforDog/9
        [HttpGet]
        public TrickDto ListTricksforDog(int id)
        {
            //all dogs that have dogtricks that match with our id
            List<Trick> tricks = db.Tricks.Where(trick => trick.Dogs.Any(dogtrick => dogtrick.DogTrickId == id)).ToList();
            List<TrickDto> TrickDtos = new List<TrickDto>();

            tricks.ForEach(trick => TrickDtos.Add(new TrickDto()
            {
                TrickId = trick.TrickId,
                TrickName = trick.TrickName,
                TrickDescription = trick.TrickDescription,
                TrickDifficulty = trick.TrickDifficulty,
                TrickVideoLink = trick.TrickVideoLink
            }));

            return TrickDtos.First();
        }

        // PUT: api/TrickData/UpdateTrick/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTrick(int id, Trick trick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trick.TrickId)
            {
                return BadRequest();
            }

            db.Entry(trick).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrickExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TrickData/AddTrick
        [ResponseType(typeof(Trick))]
        [HttpPost]
        public IHttpActionResult AddTrick(Trick trick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tricks.Add(trick);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trick.TrickId }, trick);
        }

        // DELETE: api/TrickData/DeleteTrick
        [ResponseType(typeof(Trick))]
        [HttpPost]
        public IHttpActionResult DeleteTrick(int id)
        {
            Trick trick = db.Tricks.Find(id);
            if (trick == null)
            {
                return NotFound();
            }

            db.Tricks.Remove(trick);
            db.SaveChanges();

            return Ok(trick);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrickExists(int id)
        {
            return db.Tricks.Count(e => e.TrickId == id) > 0;
        }
    }
}