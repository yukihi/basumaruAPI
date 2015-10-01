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
    public class JikokuhyousController : ApiController
    {
        private basumaruEntities db = new basumaruEntities();

        // GET: api/Jikokuhyous
        public IQueryable<Jikokuhyou> GetJikokuhyou()
        {
            return db.Jikokuhyou;
        }

        // GET: api/Jikokuhyous/5
        [ResponseType(typeof(Jikokuhyou))]
        public IHttpActionResult GetJikokuhyou(int id)
        {
            Jikokuhyou jikokuhyou = db.Jikokuhyou.Find(id);
            if (jikokuhyou == null)
            {
                return NotFound();
            }

            return Ok(jikokuhyou);
        }

        // GET: api/Jikokuhyous/[運営企業]/[路線名]/[路線名]/[行き先]/[日付分類]/
        //create ookubo.jin
        [ResponseType(typeof(Jikokuhyou))]
        public IHttpActionResult GetJikokuhyou(string kigyou,string rosenmei,string ikisaki,string hidukebunrui)
        {

            //[運営企業]/[路線名]/[行き先]/[日付分類]で検索
            var jikokuhyou = from p in db.Jikokuhyou
                       where p.kigyou == kigyou & p.rosenmei == rosenmei & p.ikisaki == ikisaki & p.hidukebunrui == hidukebunrui
                       orderby p.JikokuhyouId
                       select p;

            //foreach (Jikokuhyou ji in jikokuhyou) {
            //    return Ok(ji);
            //}

            //if (jikokuhyou == null)
            //{
            //    return NotFound();
            //}

            return Ok(jikokuhyou);

        }

        // PUT: api/Jikokuhyous/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJikokuhyou(int id, Jikokuhyou jikokuhyou)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jikokuhyou.JikokuhyouId)
            {
                return BadRequest();
            }

            db.Entry(jikokuhyou).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JikokuhyouExists(id))
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

        // POST: api/Jikokuhyous
        [ResponseType(typeof(Jikokuhyou))]
        public IHttpActionResult PostJikokuhyou(Jikokuhyou jikokuhyou)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jikokuhyou.Add(jikokuhyou);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jikokuhyou.JikokuhyouId }, jikokuhyou);
        }

        // DELETE: api/Jikokuhyous/5
        [ResponseType(typeof(Jikokuhyou))]
        public IHttpActionResult DeleteJikokuhyou(int id)
        {
            Jikokuhyou jikokuhyou = db.Jikokuhyou.Find(id);
            if (jikokuhyou == null)
            {
                return NotFound();
            }

            db.Jikokuhyou.Remove(jikokuhyou);
            db.SaveChanges();

            return Ok(jikokuhyou);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JikokuhyouExists(int id)
        {
            return db.Jikokuhyou.Count(e => e.JikokuhyouId == id) > 0;
        }
    }
}