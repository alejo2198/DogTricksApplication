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

namespace DogTricksApplication.Controllers
{
    public class DogxTrickDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DogxTrickData
        public IQueryable<DogxTrick> GetDogxTricks()
        {
            return db.DogxTricks;
        }

        // GET: api/DogxTrickData/5
        [ResponseType(typeof(DogxTrick))]
        public IHttpActionResult GetDogxTrick(int id)
        {
            DogxTrick dogxTrick = db.DogxTricks.Find(id);
            if (dogxTrick == null)
            {
                return NotFound();
            }

            return Ok(dogxTrick);
        }

        // PUT: api/DogxTrickData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDogxTrick(int id, DogxTrick dogxTrick)
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

        // POST: api/DogxTrickData
        [ResponseType(typeof(DogxTrick))]
        public IHttpActionResult PostDogxTrick(DogxTrick dogxTrick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DogxTricks.Add(dogxTrick);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dogxTrick.DogTrickId }, dogxTrick);
        }

        // DELETE: api/DogxTrickData/5
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