using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DogTricksApplication.Models;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace DogTricksApplication.Controllers
{
    public class DogxTrickDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DogxTrickData/ListDogxTricks
        [HttpGet]
        public IEnumerable<DogxTrick> ListDogxTricks()
        {
            return db.DogxTricks.ToList();
        }

        // GET: api/DogxTrickData/ListDogxTricksforDog/id
        [HttpGet]
        public IEnumerable<DogxTrickDto> ListDogxTricksforDog(int id)
        {
            //all dogs that have dogtricks that match with our id
            List<DogxTrick> DogxTricks = db.DogxTricks.Where(dogtrick => dogtrick.Dogs.Any(dog=> dog.DogId == id)).ToList();

            List<DogxTrickDto> DogTrickDtos = new List<DogxTrickDto>();
            DogxTricks.ForEach(dogtrick => DogTrickDtos.Add(new DogxTrickDto()
            {
                DogTrickId = dogtrick.DogTrickId,
                DogTrickDate = dogtrick.DogTrickDate
            }));
            
            return DogTrickDtos;
        }

        // GET: api/DogxTrickData/ListDogxTricksforTricks/9
        [HttpGet]
        public IEnumerable<DogxTrickDto> ListDogxTricksforTricks(int id)
        {
            //all dogs that have dogtricks that match with our id
            List<DogxTrick> DogxTricks = db.DogxTricks.Where(dogtrick => dogtrick.Tricks.Any(trick => trick.TrickId == id)).ToList();
            List<DogxTrickDto> DogTrickDtos = new List<DogxTrickDto>();

            DogxTricks.ForEach(dogtrick => DogTrickDtos.Add(new DogxTrickDto()
            {
                DogTrickId = dogtrick.DogTrickId,
                DogTrickDate = dogtrick.DogTrickDate
            }));

            return DogTrickDtos;
        }

 

        [HttpGet]
        // GET: api/DogxTrickData/FIndDogxTrick/5
        [ResponseType(typeof(DogxTrick))]
        public IHttpActionResult FindDogxTrick(int id)
        {
            DogxTrick dogxTrick = db.DogxTricks.Find(id);
            if (dogxTrick == null)
            {
                return NotFound();
            }

            return Ok(dogxTrick);
        }

        // PUT: api/DogxTrickData/UpdateDogxTrick/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDogxTrick(int id, DogxTrick dogxTrick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dogxTrick.DogTrickId)
            {
                return BadRequest();
            }

            db.Entry(dogxTrick).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogxTrickExists(id))
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
        
        // POST: api/DogxTrickData/AddDogxTrick
        [ResponseType(typeof(DogxTrick))]
        public IHttpActionResult AddDogxTrick(DogxTrick dogxTrick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           db.DogxTricks.Add(dogxTrick);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dogxTrick.DogTrickId }, dogxTrick);
        }

        // DELETE: api/DogxTrickData/DeleteDogxTrick/5
        [ResponseType(typeof(DogxTrick))]
        public IHttpActionResult DeleteDogxTrick(int id)
        {
            DogxTrick dogxTrick = db.DogxTricks.Find(id);
            if (dogxTrick == null)
            {
                return NotFound();
            }

            db.DogxTricks.Remove(dogxTrick);
            db.SaveChanges();

            return Ok(dogxTrick);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DogxTrickExists(int id)
        {
            return db.DogxTricks.Count(e => e.DogTrickId == id) > 0;
        }
    }
}