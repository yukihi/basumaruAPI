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
using basumaruAPI.Models;
using System.Web.Http.Cors;

namespace basumaruAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BasuteisController : ApiController
    {
        private basumaruEntities db = new basumaruEntities();

        // GET: api/Basuteis
        public IQueryable<Basutei> GetBasutei()
        {
            return db.Basutei;
        }

        // GET: api/Basuteis/5
        [ResponseType(typeof(Basutei))]
        public IHttpActionResult GetBasutei(int id)
        {
            Basutei basutei = db.Basutei.Find(id);
            if (basutei == null)
            {
                return NotFound();
            }

            return Ok(basutei);
        }

        // GET: api/Basuteis/[運営企業]/[路線名]/
        //create ookubo.jin
        [ResponseType(typeof(Basutei))]
        public IHttpActionResult GetBasutei(string kigyou,string rosenmei)
        {

            //[運営企業]/[路線名]で検索
            var basutei = from p in db.Basutei
                       where p.kigyou == kigyou & p.rosenmei == rosenmei
                       orderby p.ido
                       select p;

            //foreach (Basutei bs in basutei) {
            //    return Ok(bs);
            //}

            //if (basutei == null)
            //{
            //    return NotFound();
            //}

            return Ok(basutei);

        }

        // PUT: api/Basuteis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBasutei(int id, Basutei basutei)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != basutei.BasuteiId)
            {
                return BadRequest();
            }

            db.Entry(basutei).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasuteiExists(id))
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

        // POST: api/Basuteis
        [ResponseType(typeof(Basutei))]
        public IHttpActionResult PostBasutei(Basutei basutei)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Basutei.Add(basutei);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = basutei.BasuteiId }, basutei);
        }

        // DELETE: api/Basuteis/5
        [ResponseType(typeof(Basutei))]
        public IHttpActionResult DeleteBasutei(int id)
        {
            Basutei basutei = db.Basutei.Find(id);
            if (basutei == null)
            {
                return NotFound();
            }

            db.Basutei.Remove(basutei);
            db.SaveChanges();

            return Ok(basutei);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BasuteiExists(int id)
        {
            return db.Basutei.Count(e => e.BasuteiId == id) > 0;
        }
    }
}