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
using System.Web.Routing;

namespace DogTricksApplication.Controllers
{
    public class DogxTrickDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all dogsxTricks in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all dogsxTricks in the database,
        /// </returns>
        /// <example>
        ///GET: api/DogxTrickData/ListDogxTricks
        /// </example>
        
        [HttpGet]
        public IEnumerable<DogxTrick> ListDogxTricks()
        {
            return db.DogxTricks.ToList();
        }


        /// <summary>
        /// lists all dog tricks per dog
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all dogtricks per dog
        /// </returns>
        /// <example>
        ///GET: api/DogxTrickData/ListDogxTricksforDog/id
        /// </example>
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

        /// <summary>
        /// lists all dog tricks per trick
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all dogtricks per trick
        /// </returns>
        /// <example>
        ///GET: api/DogxTrickData/ListDogxTricksforTricks/9
        /// </example>

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


        /// <summary>
        /// finds one dogxtrick by id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT:one dogxtrick
        /// </returns>
        /// <example>
        ///GET: api/DogxTrickData/FIndDogxTrick/5
        /// </example>
        [HttpGet]
        
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

        /// <summary>
        /// updates one dogxtrick by id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT:one dogxtrick
        /// </returns>
        /// <example>
        ///PUT: api/DogxTrickData/UpdateDogxTrick/5
        /// </example>

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

        /// <summary>
        /// adds one dogxtrick to database
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT:one dogxtrick
        /// </returns>
        /// <example>
        /// POST: api/DogxTrickData/AddDogxTrick
        /// </example>

        [ResponseType(typeof(DogxTrick))]
        [HttpPost]
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


        /// <summary>
        ///grabs the latest dogxtrick and attaches it to the dog by id and the trick by id. 
        ///Completes the bridge tables
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT:one dogxtrick,one dog,one trick
        /// </returns>
        /// <example>
        /// POST: api/DogxTrickData/connecttricktodog/{DogId}/{TrickId}
        /// </example>

        [HttpPost]
        [Route("api/dogxtrickdata/connecttricktodog/{DogId}/{TrickId}")]

        public IHttpActionResult ConnectTricktoDog(int DogId, int TrickId)
        {
            DogxTrick lastdogtrick = db.DogxTricks.OrderByDescending(u => u.DogTrickId).FirstOrDefault();
            
            Dog selectedDog = db.Dogs.Include(dog => dog.Tricks).Where(d => d.DogId == DogId).FirstOrDefault();
            selectedDog.Tricks.Add(lastdogtrick);

            Trick selectedTrick = db.Tricks.Include(trick => trick.Dogs).Where(t => t.TrickId == TrickId).FirstOrDefault();
            selectedTrick.Dogs.Add(lastdogtrick);

            db.SaveChanges();
            return Ok();
        }


        /// <summary>
        ///deletes dogxtrick by id
        ///Completes the bridge tables
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT:one dogxtrick
        /// </returns>
        /// <example>
        /// DELETE: api/DogxTrickData/DeleteDogxTrick/5
        /// </example>
       
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