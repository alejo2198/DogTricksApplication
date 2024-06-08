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

namespace DogDogsApplication.Controllers
{
    public class DogDataController : ApiController
    {
          
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DogData/ListDogs
        [HttpGet]
        public IEnumerable<DogDto> ListDogs()
        {
            List < Dog > Dogs = db.Dogs.ToList();
            List<DogDto> DogDtos = new List<DogDto>();

            Dogs.ForEach(dog => DogDtos.Add(new DogDto()
            {
                DogId = dog.DogId,
                DogName = dog.DogName,
                DogAge = dog.DogAge,
                DogBreed = dog.DogBreed,
                DogBirthday = dog.DogBirthday
            }));

            return DogDtos;
        }

        // GET: api/DogData/FindDog/5
        [ResponseType(typeof(Dog))]
        [HttpGet]
        public IHttpActionResult FindDog(int id)
        {
            Dog dog = db.Dogs.Find(id);
           
            if (dog == null)
            {
                return NotFound();
            }
            DogDto DogDto = new DogDto()
            {
                DogId = dog.DogId,
                DogName = dog.DogName,
                DogAge = dog.DogAge,
                DogBreed = dog.DogBreed,
                DogBirthday = dog.DogBirthday
            };
            return Ok(DogDto);
        }

        // PUT: api/DogData/UpdateDog/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDog(int id, Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dog.DogId)
            {
                return BadRequest();
            }

            db.Entry(dog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
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

        // POST: api/DogData/AddDog
        [ResponseType(typeof(Dog))]
        [HttpPost]
        public IHttpActionResult AddDog(Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dogs.Add(dog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dog.DogId }, dog);
        }

        // DELETE: api/DogData/DeleteDog/5
        [ResponseType(typeof(Dog))]
        [HttpPost]
        public IHttpActionResult DeleteDog(int id)
        {
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return NotFound();
            }

            db.Dogs.Remove(dog);
            db.SaveChanges();

            return Ok(dog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DogExists(int id)
        {
            return db.Dogs.Count(e => e.DogId == id) > 0;
        }
    }
}

